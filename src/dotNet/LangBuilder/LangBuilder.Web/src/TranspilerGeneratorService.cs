using LangBuilder.Source.Domain;
using LangBuilder.Source.Models;
using LangBuilder.Source.Service;

namespace LangBuilder.Web.src
{
    public class TranspilerGeneratorService
    {
        private readonly AntlrGeneratorService _antlrGeneratorService;
        private readonly ExecutableGeneratorService _executableGeneratorService;
        private readonly GeneratorConfiguration _generatorConfiguration;
        private readonly GrammarFileGeneratorService _grammarFileGeneratorService;
        private readonly TranspilerRuleService _transpilerRuleService;

        public TranspilerGeneratorService(AntlrGeneratorService antlrGeneratorService, ExecutableGeneratorService executableGeneratorService, IOptions<GeneratorConfiguration> generatorConfigurationOptions, GrammarFileGeneratorService grammarFileGeneratorService, TranspilerRuleService transpilerRuleService)
        {
            _antlrGeneratorService = antlrGeneratorService;
            _executableGeneratorService = executableGeneratorService;
            _grammarFileGeneratorService = grammarFileGeneratorService;
            _transpilerRuleService = transpilerRuleService;
            _generatorConfiguration = generatorConfigurationOptions.Value;
        }

        public async Task<object> TestGenerateTranspiler(TranspilerViewModel viewModel)
        {
            viewModel.GrammarName = "TranspilerGrammar";

            var model = new TranspilerModel
            {
                GrammarName = "TranspilerGrammar",
                Name = viewModel.Name,
                Rules = await _transpilerRuleService.ProcessRules(viewModel.RuleSet)
            };

            await _grammarFileGeneratorService.GenerateGrammarFile(model);
            var antlrResult = await _antlrGeneratorService.GenerateAntlrFiles();

            var executableResult = await _executableGeneratorService.GenerateExecutable(model);

            return new
            {
                antlr = antlrResult,
                exec = executableResult
            };
        }
    }
}
