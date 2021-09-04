using System.IO;
using System.Threading.Tasks;
using LangBuilder.Controllers.Shared;
using LangBuilder.Models;
using LangBuilder.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LangBuilder.Controllers
{
    [ApiController]
    [Route("api/generate")]
    public class GenerateApiController : BaseApiController
    {
        private readonly AntlrGeneratorService _antlrGeneratorService;
        private readonly ExecutableGeneratorService _executableGeneratorService;
        private readonly GeneratorConfiguration _generatorConfiguration;

        public GenerateApiController(AntlrGeneratorService antlrGeneratorService, ExecutableGeneratorService executableGeneratorService, IOptions<GeneratorConfiguration> generatorConfigurationOptions)
        {
            _antlrGeneratorService = antlrGeneratorService;
            _executableGeneratorService = executableGeneratorService;
            _generatorConfiguration = generatorConfigurationOptions.Value;
        }

        [Route("test")]
        public async Task<AntlrGenerateOutputModel> TestGenerate()
        {
            var result = _antlrGeneratorService.GenerateAntlrFiles();

            return result;
        }

        [Route("test-code")]
        public async Task<GenerateExecutableOutputModel> TestGenerateCode()
        {
            var result = await _executableGeneratorService.GenerateExecutable();

            return result;
        }

        [Route("test-download")]
        public async Task<FileResult> TestFileDownload()
        {
            var path = _generatorConfiguration.ExecutablePath;
            var bytes = System.IO.File.ReadAllBytes(path);
            var contentType = "application/octet-stream";

            return File(bytes, contentType, path);
        }
    }
}
