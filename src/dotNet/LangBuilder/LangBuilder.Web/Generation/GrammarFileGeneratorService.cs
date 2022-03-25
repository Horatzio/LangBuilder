using LangBuilder.Source.Domain;
using LangBuilder.Source.Extensions;
using Scriban;
using Scriban.Runtime;

namespace LangBuilder.Source.Service
{
    public class GrammarFileGeneratorService
    {
        private readonly GeneratorConfiguration _configuration;

        public GrammarFileGeneratorService(GeneratorConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task GenerateGrammarFile(TranspilerModel model)
        {
            var grammarFilePath = _configuration.GrammarFilePath;

            var templatePath = "Generation/GrammarFileTemplate.g4";
            var templateContent = File.ReadAllText(templatePath);

            var scriptObj = new ScriptObject();
            scriptObj.Import("toCamel", new Func<string, string>((s) => s.ToLowercaseFirstLetter()));
            scriptObj.Add("model", model);
            var template = Template.Parse(templateContent);

            var context = new TemplateContext();
            context.PushGlobal(scriptObj);
            var result = template.Render(context);

            await File.WriteAllTextAsync(grammarFilePath, result);
        }
    }
}
