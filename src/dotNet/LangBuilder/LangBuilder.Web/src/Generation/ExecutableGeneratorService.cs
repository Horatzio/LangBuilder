using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LangBuilder.Models;
using LangBuilder.Source.Domain;
using LangBuilder.Source.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Extensions.Options;

namespace LangBuilder.Source.Service
{
    public class ExecutableGeneratorService
    {
        private readonly GeneratorConfiguration _generatorConfiguration;

        public ExecutableGeneratorService(IOptions<GeneratorConfiguration> configuration)
        {
            _generatorConfiguration = configuration.Value;
        }

        public async Task<GenerateExecutableOutputModel> GenerateExecutable(TranspilerModel model)
        {
            var grammarName = $"{model.GrammarName}";

            var transpilerProgramPath = _generatorConfiguration.TranspilerProgramPath;

            var transpilerProjectPath = Path.Combine(transpilerProgramPath, "Transpiler.csproj");

            var executablePath = Path.Combine(Path.GetDirectoryName(_generatorConfiguration.ExecutablePath), $"{model.Name}{Path.GetExtension(_generatorConfiguration.ExecutablePath)}");

            GenerateConcreteVisitorImplementations(grammarName, transpilerProgramPath, model.Rules);

            using var workspace = MSBuildWorkspace.Create();

            var project = await workspace.OpenProjectAsync(transpilerProjectPath);

            var compilation = await project.GetCompilationAsync();

            var GCSettingsAssembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location;

            var dotNetCoreDir = Path.GetDirectoryName(GCSettingsAssembly);

            var netstandard = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "netstandard");

            var antlrRuntimePath = Path.Combine(transpilerProgramPath, "Antlr4.Runtime.Standard.dll");

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
                    MetadataReference.CreateFromFile(antlrRuntimePath)
                );

            var result = compilation.Emit(executablePath);
            
            var runtimeConfig = GenerateRuntimeConfig();
            await File.WriteAllTextAsync(Path.ChangeExtension(executablePath, ".runtimeconfig.json"), runtimeConfig);

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
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(
                stream,
                new JsonWriterOptions {Indented = true}
            );

            writer.WriteStartObject();
            writer.WriteStartObject("runtimeOptions");
            writer.WriteStartObject("framework");
            writer.WriteString("name", "Microsoft.NETCore.App");
            writer.WriteString(
                "version",
                RuntimeInformation.FrameworkDescription.Replace(".NET Core ", "")
            );
            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.WriteEndObject();

            writer.Flush();

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}
