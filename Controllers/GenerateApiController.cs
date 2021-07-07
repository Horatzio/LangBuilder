using System.Threading.Tasks;
using LangBuilder.Controllers.Shared;
using LangBuilder.Service;
using Microsoft.AspNetCore.Mvc;

namespace LangBuilder.Controllers
{
    [ApiController]
    [Route("api/generate")]
    public class GenerateApiController : BaseApiController
    {
        private readonly ParserService _parserService;

        public GenerateApiController(ParserService parserService)
        {
            _parserService = parserService;
        }

        [Route("test")]
        public async Task<ActionResult> TestGenerate()
        {
            var message = _parserService.GenerateParser();

            return Ok(message);
        }
    }
}
