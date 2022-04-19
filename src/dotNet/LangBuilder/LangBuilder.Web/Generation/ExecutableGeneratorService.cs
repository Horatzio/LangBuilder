using System.Reflection;
using LangBuilder.Models;
using LangBuilder.Source.Domain;
using LangBuilder.Source.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace LangBuilder.Source.Service
{
    public class ExecutableGeneratorService
    {
        private readonly GeneratorConfiguration generatorConfiguration;

        public ExecutableGeneratorService(GeneratorConfiguration configuration)
        {
            generatorConfiguration = configuration;
        }

        public async Task<GenerateExecutableOutputModel> GenerateExecutable(TranspilerModel model, string executablePath)
        {
            var grammarName = "TranspilerGrammar";
            var transpilerProjectPath = generatorConfiguration.TranspilerProjectPath;
            var transpilerProjectFilePath = Path.Combine(transpilerProjectPath, "Transpiler.csproj");

            GenerateConcreteVisitorImplementations(grammarName, transpilerProjectPath, model.Rules);

            using var workspace = MSBuildWorkspace.Create();
            var project = await workspace.OpenProjectAsync(transpilerProjectFilePath);
            project = EnsureSourcesInProject(project, transpilerProjectPath);

            var compilation = await project.GetCompilationAsync();


            var GCSettingsAssembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location;
            var dotNetCoreDir = Path.GetDirectoryName(GCSettingsAssembly);
            var netstandard = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "netstandard");
            var antlrRuntimePath = Path.Combine(transpilerProjectPath, "Antlr4.Runtime.Standard.dll");

            compilation = compilation
                .AddReferences(
                    MetadataReference.CreateFromFile(GCSettingsAssembly),
                    MetadataReference.CreateFromFile(typeof(Console).GetTypeInfo().Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(File).GetTypeInfo().Assembly.Location),
                    MetadataReference.CreateFromFile(netstandard.Location),
                    MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "System.Runtime.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "System.Runtime.Extensions.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "System.Linq.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "System.Collections.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "System.Private.CoreLib.dll")),
                    MetadataReference.CreateFromFile(antlrRuntimePath)
                );

            var result = compilation.Emit(executablePath);

            if (!result.Success)
                return ResultToOutputModel(result);

            var runtimeConfig = GenerateRuntimeConfig();
            await File.WriteAllTextAsync(Path.ChangeExtension(executablePath, ".runtimeconfig.json"), runtimeConfig);
            return ResultToOutputModel(result);

            static GenerateExecutableOutputModel ResultToOutputModel(Microsoft.CodeAnalysis.Emit.EmitResult result)
            {
                return new GenerateExecutableOutputModel
                {
                    Success = result.Success,
                    Errors = result.Diagnostics.Select(d => new GenerateExecutableDiagnosticError
                    {
                        Severity = d.Severity.ToString(),
                        ErrorId = d.Descriptor.Id,
                        Description = d.Descriptor.Category,
                        Message = d.GetMessage(),
                        Location = new GenerateExecutableDiagnosticErrorLocation
                        {
                            StartLine = d.Location.GetLineSpan().StartLinePosition.Line,
                            StartPosition = d.Location.GetLineSpan().StartLinePosition.Character,
                            EndLine = d.Location.GetLineSpan().EndLinePosition.Line,
                            EndPosition = d.Location.GetLineSpan().EndLinePosition.Character
                        }
                    })
                };
            }
        }

        private Project EnsureSourcesInProject(Project project, string transpilerProjectPath)
        {
            var files = Directory.GetFiles(transpilerProjectPath, "*.cs", SearchOption.TopDirectoryOnly)
                .Concat(Directory.GetFiles(Path.Combine(transpilerProjectPath, "Output"), "*.cs", SearchOption.TopDirectoryOnly));

            var unincludedFiles = files.Where(f => !project.Documents.Any(d => d.Name == Path.GetFileName(f)));

            foreach(var file in files)
            {
                project = project.AddDocument(Path.GetFileName(file), File.ReadAllText(file)).Project;
            }

            return project;
        }

        private void GenerateConcreteVisitorImplementations(string grammarName, string transpilerProgramPath, IEnumerable<TranspilerRule> rules)
        {
            var concreteVisitorName = $"{grammarName}ConcreteVisitor";

            var concreteVisitorPath = $"{transpilerProgramPath}\\{concreteVisitorName}GeneratedMethods.cs";

            var concreteVisitorSyntaxTree = SyntaxFactory.ParseSyntaxTree(File.ReadAllText(concreteVisitorPath));

            var parserName = $"{grammarName}Parser";

            var stringType = SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword));

            var methods = rules.Select(rule =>
            {
                var modifiers = new SyntaxTokenList();
                modifiers = modifiers.Add(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                modifiers = modifiers.Add(SyntaxFactory.Token(SyntaxKind.OverrideKeyword));

                var body = SyntaxFactory.Block(SyntaxFactory.ParseStatement(rule.RuleBody)
                    .NormalizeWhitespace());

                SeparatedSyntaxList<ParameterSyntax> parametersList = new SeparatedSyntaxList<ParameterSyntax>().AddRange
                (new []
                    {
                        SyntaxFactory.Parameter(SyntaxFactory.Identifier("context")).WithType(SyntaxFactory.ParseTypeName($"{parserName}.{rule.Name.ToCapitalizedFirstLetter()}Context"))
                    }
                );

                var parameterListSyntax = SyntaxFactory.ParameterList(parametersList);

                var method = SyntaxFactory.MethodDeclaration(stringType, SyntaxFactory.Identifier($"Visit{rule.Name.ToCapitalizedFirstLetter()}"))
                    .WithModifiers(modifiers)
                    .WithParameterList(parameterListSyntax)
                    .WithBody(body)
                    .NormalizeWhitespace();

                return method;
            });

            var concreteVisitorChildNodes = concreteVisitorSyntaxTree.GetRoot()
                .DescendantNodes();

            var concreteVisitorClass = concreteVisitorChildNodes
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault(c => c.Identifier.Text == concreteVisitorName);

            var newConcreteVisitorClass = concreteVisitorClass
                .WithMembers(new SyntaxList<MemberDeclarationSyntax>(methods))
                .NormalizeWhitespace();

            var concreteVisitorRoot = concreteVisitorSyntaxTree.GetRoot();

            concreteVisitorRoot = concreteVisitorRoot.ReplaceNode(concreteVisitorClass, newConcreteVisitorClass);

            concreteVisitorSyntaxTree =
                concreteVisitorSyntaxTree.WithRootAndOptions(concreteVisitorRoot.NormalizeWhitespace(), concreteVisitorSyntaxTree.Options);

            File.WriteAllText(concreteVisitorPath, concreteVisitorSyntaxTree.ToString());
        }

        private static string GenerateRuntimeConfig()
        {
            return File.ReadAllText("Generation/.runtimeconfig.json");
        }
    }
}
