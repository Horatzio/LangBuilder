using System.Diagnostics;
using LangBuilder.Models;

namespace LangBuilder.Source.Service
{
    public class AntlrGeneratorService
    {
        private readonly GeneratorConfiguration configuration;
        public AntlrGeneratorService(GeneratorConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ClearOutputDirectory(string antlrOutputPath)
        {
            var directory = new DirectoryInfo(antlrOutputPath);

            var files = directory.GetFiles();
            var subDirectories = directory.GetDirectories();

            foreach (var file in files)
            {
                file.Delete();
            }
            foreach (var subDirectory in subDirectories)
            {
                subDirectory.Delete(true);
            }
        }

        public async Task<AntlrGenerateOutputModel> GenerateAntlrFiles()
        {
            ClearOutputDirectory(configuration.AntlrOutputPath);

            var arguments = string.Format(configuration.AntlrCommandLine, configuration.GrammarFilePath,
                configuration.AntlrOutputPath);

            var processStartInfo = new ProcessStartInfo(configuration.AntlrPath, arguments)
            {
                RedirectStandardOutput = true, 
                RedirectStandardError = true,
                WorkingDirectory = Path.GetDirectoryName(configuration.AntlrPath)
            };

            using var process = Process.Start(processStartInfo);

            if (process == null)
                throw new ApplicationException("Process did not start successfully");

            process.WaitForExit();

            var exitCode = process.ExitCode;
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();

            return new AntlrGenerateOutputModel
            {
                ExitCode = exitCode,
                Output = output,
                Error = error
            };
        }
    }
}
