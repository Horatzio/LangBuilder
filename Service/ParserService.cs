using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace LangBuilder.Service
{
    public class ParserService
    {
        private readonly LangBuilderConfiguration _configuration;
        public ParserService(IOptions<LangBuilderConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public string GenerateParser()
        {
            var arguments = $"{_configuration.GrammarFilePath} -o {_configuration.AntlrOutputPath} -Xexact-output-dir";
            var processStartInfo = new ProcessStartInfo(_configuration.AntlrPath, arguments);
            processStartInfo.RedirectStandardOutput = true;
            var process = Process.Start(processStartInfo);

            var commandLineText = process.StandardOutput.ReadToEnd();
            process.WaitForExit(500);

            return commandLineText;
        }
    }
}
