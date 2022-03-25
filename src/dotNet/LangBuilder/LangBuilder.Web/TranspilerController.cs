using LangBuilder.Source.Models;
using Microsoft.AspNetCore.Mvc;

namespace LangBuilder.Web
{
    [ApiController]
    [Route("[controller]")]
    public class TranspilerController
    {
        private readonly TranspilerGeneratorService service;

        public TranspilerController(TranspilerGeneratorService service)
        {
            this.service = service;
        }

        [HttpPost("test-transpile")]
        public async Task<object> TestTranspile([FromBody] TranspilerViewModel model)
        {
            return await service.TestGenerateTranspiler(model);
        }
    }
}
