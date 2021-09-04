using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LangBuilder.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Extensions.Options;

namespace LangBuilder.Service
{
    public class ExecutableGeneratorService
    {
        private readonly GeneratorConfiguration _generatorConfiguration;

        public ExecutableGeneratorService(IOptions<GeneratorConfiguration> configuration)
        {
            _generatorConfiguration = configuration.Value;
        }

        public async Task<GenerateExecutableOutputModel> GenerateExecutable()
        {
            //GenerateConcreteVisitor();

            var transpilerProgramPath = _generatorConfiguration.TranspilerProgramPath;

            var transpilerProjectPath = Path.Combine(transpilerProgramPath, "Transpiler.csproj");

            var executablePath = _generatorConfiguration.ExecutablePath;

            var antlrRuntimePath = Path.Combine(transpilerProgramPath, "Antlr4.Runtime.Standard.dll");

            //var outputPath = _generatorConfiguration.AntlrOutputPath;

            //var grammarName = "TranspilerGrammar";

            //var baseVisitorName = $"{grammarName}BaseVisitor";

            //var baseVisitorPath = $"{outputPath}\\{baseVisitorName}.cs";

            //var concreteVisitorName = $"{grammarName}ConcreteVisitor";

            //var concreteVisitorPath = $"{outputPath}\\{concreteVisitorName}.cs";

            //var mainPath = $"{transpilerProgramPath}\\Program.cs";

            //var baseVisitorSyntaxTree =
            //    SyntaxFactory.ParseSyntaxTree(SourceText.From(File.ReadAllText(baseVisitorPath)));

            //var concreteVisitorSyntaxTree =
            //    SyntaxFactory.ParseSyntaxTree(SourceText.From(File.ReadAllText(concreteVisitorPath)));

            //var mainSyntaxTree = SyntaxFactory.ParseSyntaxTree(SourceText.From(File.ReadAllText(mainPath)));

            //var executablePath = _generatorConfiguration.ExecutablePath;

            //File.Create(executablePath).Close();

            //var assemblyPath = executablePath;


            //var references = typeof(TranspilerGrammarConcreteVisitor).Assembly.GetReferencedAssemblies();

            //var compilation = CSharpCompilation.Create(Path.GetFileName(assemblyPath))
            //    .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
            //    .AddReferences(
            //        MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
            //        MetadataReference.CreateFromFile(typeof(Console).GetTypeInfo().Assembly.Location),
            //        MetadataReference.CreateFromFile(typeof(TranspilerGrammarConcreteVisitor).GetTypeInfo().Assembly.Location),
            //        MetadataReference.CreateFromFile(typeof()
            //        MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "System.Runtime.dll"))
            //    )
            //    .AddSyntaxTrees(mainSyntaxTree, concreteVisitorSyntaxTree);

            //var result = compilation.Emit(assemblyPath);

            //File.WriteAllText(
            //    Path.ChangeExtension(assemblyPath, "runtimeconfig.json"),
            //    GenerateRuntimeConfig()
            //);

            //var process = Process.Start("dotnet", assemblyPath);

            using (var workspace = MSBuildWorkspace.Create())
            {
                workspace.WorkspaceFailed += (sender, args) =>
                {
                    Console.WriteLine(args.Diagnostic.Message);
                };

                var project = await workspace.OpenProjectAsync(transpilerProjectPath);

                var compilation = await project.GetCompilationAsync();

                var GCSettingsAssembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location;

                var dotNetCoreDir = Path.GetDirectoryName(GCSettingsAssembly);

                var netstandard = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "netstandard");

                var dotNetCoreDirEntries = Directory.EnumerateFiles(dotNetCoreDir);

                var systemReferences = dotNetCoreDirEntries.Select(e => MetadataReference.CreateFromFile(e));

                compilation = compilation
                    .AddReferences(
                        MetadataReference.CreateFromFile(GCSettingsAssembly),
                        MetadataReference.CreateFromFile(typeof(Console).GetTypeInfo().Assembly.Location),
                        MetadataReference.CreateFromFile(netstandard.Location),
                        MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "System.Runtime.dll")),
                        MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "System.Runtime.Extensions.dll")),
                        MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "System.Linq.dll")),
                        MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "System.Collections.dll")),
                        MetadataReference.CreateFromFile(antlrRuntimePath)
                    );

                var result = compilation.Emit(executablePath);

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

        private void GenerateConcreteVisitor()
        {
            //var transpilerProgramPath = _generatorConfiguration.TranspilerProgramPath;
            //var outputPath = _generatorConfiguration.AntlrOutputPath;

            //var grammarName = "TranspilerGrammar";

            //var baseVisitorName = $"{grammarName}BaseVisitor";

            //var baseVisitorPath = $"{outputPath}\\{baseVisitorName}.cs";

            //var concreteVisitorName = $"{grammarName}ConcreteVisitor";

            //var concreteVisitorPath = $"{outputPath}\\{concreteVisitorName}.cs";

            //var baseVisitorSyntaxTree =
            //    SyntaxFactory.ParseSyntaxTree(SourceText.From(File.ReadAllText(baseVisitorPath)));

            //var baseVisitorRoot = baseVisitorSyntaxTree.GetRoot();

            //var baseVisitorNodes = baseVisitorRoot.ChildNodes();

            //baseVisitorNodes = baseVisitorNodes.Append(baseVisitorRoot);

            //var baseVisitorClassDeclaration = baseVisitorNodes.OfType<ClassDeclarationSyntax>().FirstOrDefault();

            //var concreteVisitorClassDeclaration = SyntaxFactory.ClassDeclaration(concreteVisitorName);

            //concreteVisitorClassDeclaration = concreteVisitorClassDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            //concreteVisitorClassDeclaration = concreteVisitorClassDeclaration.AddBaseListTypes(
            //    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseName($"{baseVisitorName}<string>"))
            //);

            //var methods = baseVisitorClassDeclaration.Members.OfType<MethodDeclarationSyntax>();

            //var stringType = SyntaxFactory.ParseTypeName("string");

            //foreach (var method in methods)
            //{
            //    var modifiers = new SyntaxTokenList();
            //    modifiers = modifiers.Add(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            //    modifiers = modifiers.Add(SyntaxFactory.Token(SyntaxKind.OverrideKeyword));

            //    var body = SyntaxFactory.Block(SyntaxFactory.ParseStatement("return \"\";"));

            //    var memberMethod = SyntaxFactory.MethodDeclaration(stringType, method.Identifier)
            //        .WithModifiers(modifiers)
            //        .WithParameterList(method.ParameterList)
            //        .WithBody(body);

            //    concreteVisitorClassDeclaration = concreteVisitorClassDeclaration.AddMembers(memberMethod);
            //}

            //var concreteVisitorNamespace =
            //    SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("Transpiler.Output"));

            //concreteVisitorNamespace = concreteVisitorNamespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Antlr4.Runtime.Misc")));
            //concreteVisitorNamespace = concreteVisitorNamespace.AddMembers(concreteVisitorClassDeclaration);

            //File.WriteAllText(concreteVisitorPath, concreteVisitorNamespace.NormalizeWhitespace()
            //    .ToFullString());
        }

        private string GenerateRuntimeConfig()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(
                    stream,
                    new JsonWriterOptions() { Indented = true }
                ))
                {
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
                }

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
