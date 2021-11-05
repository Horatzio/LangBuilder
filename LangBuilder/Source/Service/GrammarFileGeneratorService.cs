using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LangBuilder.Source.Domain;
using Microsoft.Extensions.Options;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace LangBuilder.Source.Service
{
    public class GrammarFileGeneratorService
    {
        private readonly GeneratorConfiguration _configuration;

        public GrammarFileGeneratorService(IOptions<GeneratorConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public async Task<IRazorEngineService> CreateRazorEngineService()
        {
            var templateManager = new ResolvePathTemplateManager(new List<string>());

            var config = new TemplateServiceConfiguration();
            config.TemplateManager = templateManager;

            return RazorEngineService.Create(config);
        }

        public async Task GenerateGrammarFile(TranspilerModel model)
        {
            var grammarFilePath = _configuration.GrammarFilePath;

            var service = await CreateRazorEngineService();

            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            var templatePath = Path.Combine(basePath, "Source", "Service", "GrammarFileTemplate.cshtml");

            var result = service.RunCompile(
                templatePath,
                typeof(TranspilerModel),
                model
            );

            await File.WriteAllTextAsync(grammarFilePath, result);
        }
    }
}
