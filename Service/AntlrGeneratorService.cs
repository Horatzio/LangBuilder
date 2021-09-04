using System.Diagnostics;
using System.IO;
using LangBuilder.Models;
using Microsoft.Extensions.Options;

namespace LangBuilder.Service
{
    public class AntlrGeneratorService
    {
        private readonly GeneratorConfiguration _configuration;
        public AntlrGeneratorService(IOptions<GeneratorConfiguration> configuration)
        {
            _configuration = configuration.Value;
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

        public AntlrGenerateOutputModel GenerateAntlrFiles()
        {
            ClearOutputDirectory(_configuration.AntlrOutputPath);

            var arguments = string.Format(_configuration.AntlrCommandLine, _configuration.GrammarFilePath,
                _configuration.AntlrOutputPath);

            var processStartInfo = new ProcessStartInfo(_configuration.AntlrPath, arguments);
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            using (var process = Process.Start(processStartInfo))
            {
                process.WaitForExit();

                var exitCode = process.ExitCode;
                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();

                return new AntlrGenerateOutputModel
                {
                    ExitCode = exitCode,
                    Output = output,
                    Error = error
                };
            }
        }
    }
}
