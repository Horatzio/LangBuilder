using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using LangBuilder.Models;
using Microsoft.Extensions.Options;

namespace LangBuilder.Source.Service
{
    public class AntlrGeneratorService
    {
        private readonly GeneratorConfiguration _configuration;
        public AntlrGeneratorService(IOptions<GeneratorConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public async Task ClearOutputDirectory(string antlrOutputPath)
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
            await ClearOutputDirectory(_configuration.AntlrOutputPath);

            var arguments = string.Format(_configuration.AntlrCommandLine, _configuration.GrammarFilePath,
                _configuration.AntlrOutputPath);

            var processStartInfo = new ProcessStartInfo(_configuration.AntlrPath, arguments)
            {
                RedirectStandardOutput = true, 
                RedirectStandardError = true
            };

            using var process = Process.Start(processStartInfo);
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
