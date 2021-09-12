using System.Threading.Tasks;
using LangBuilder.Source.Domain;
using LangBuilder.Source.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LangBuilder.Source.Controllers
{
    [ApiController]
    [Route("api/generate")]
    public class GenerateApiController : BaseApiController
    {
        private readonly AntlrGeneratorService _antlrGeneratorService;
        private readonly ExecutableGeneratorService _executableGeneratorService;
        private readonly GeneratorConfiguration _generatorConfiguration;
        private readonly GrammarFileGeneratorService _grammarFileGeneratorService;

        public GenerateApiController(AntlrGeneratorService antlrGeneratorService, ExecutableGeneratorService executableGeneratorService, IOptions<GeneratorConfiguration> generatorConfigurationOptions, GrammarFileGeneratorService grammarFileGeneratorService)
        {
            _antlrGeneratorService = antlrGeneratorService;
            _executableGeneratorService = executableGeneratorService;
            _grammarFileGeneratorService = grammarFileGeneratorService;
            _generatorConfiguration = generatorConfigurationOptions.Value;
        }

        [HttpPost("test-transpile")]
        public async Task<IActionResult> TestGenerateTranspiler([FromBody] TranspilerModel model)
        {
            model.GrammarName = "TranspilerGrammar";
            await _grammarFileGeneratorService.GenerateGrammarFile(model);
            var antlrResult = await _antlrGeneratorService.GenerateAntlrFiles();

            var executableResult = await _executableGeneratorService.GenerateExecutable(model);

            return new OkObjectResult(new
            {
                antlr = antlrResult,
                exec = executableResult
            });
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
