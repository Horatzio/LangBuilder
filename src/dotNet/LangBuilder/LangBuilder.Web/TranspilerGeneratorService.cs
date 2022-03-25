using LangBuilder.Source.Domain;
using LangBuilder.Source.Models;
using LangBuilder.Source.Service;

namespace LangBuilder.Web
{
    public class TranspilerGeneratorService
    {
        private readonly AntlrGeneratorService _antlrGeneratorService;
        private readonly ExecutableGeneratorService _executableGeneratorService;
        private readonly GrammarFileGeneratorService _grammarFileGeneratorService;
        private readonly TranspilerRuleService _transpilerRuleService;

        public TranspilerGeneratorService(AntlrGeneratorService antlrGeneratorService, ExecutableGeneratorService executableGeneratorService, GrammarFileGeneratorService grammarFileGeneratorService, TranspilerRuleService transpilerRuleService)
        {
            _antlrGeneratorService = antlrGeneratorService;
            _executableGeneratorService = executableGeneratorService;
            _grammarFileGeneratorService = grammarFileGeneratorService;
            _transpilerRuleService = transpilerRuleService;
        }

        public async Task<object> TestGenerateTranspiler(TranspilerViewModel viewModel)
        {
            var model = new TranspilerModel
            {
                GrammarName = "TranspilerGrammar",
                Name = viewModel.Name,
                Rules = _transpilerRuleService.ProcessRules(viewModel.Rules)
            };

            await _grammarFileGeneratorService.GenerateGrammarFile(model);
            var antlrResult = await _antlrGeneratorService.GenerateAntlrFiles();

            if (antlrResult.ExitCode != 0)
                return new {
                    antlrResult = antlrResult
                };

            var executableResult = await _executableGeneratorService.GenerateExecutable(model);

            return new
            {
                antlr = antlrResult,
                exec = executableResult
            };
        }
    }
}
