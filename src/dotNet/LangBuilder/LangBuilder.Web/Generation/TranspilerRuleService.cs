using LangBuilder.Source.Domain;

namespace LangBuilder.Source.Service
{
    public class TranspilerRuleService
    {
        public IEnumerable<TranspilerRule> ProcessRules(IEnumerable<TranspilerRuleViewModel> models)
        {
            var simpleRuleModels = models.Where(r => r.IsSimple);

            var simpleRules = simpleRuleModels
                .Select(TransformSimpleRule);

            var complexRuleModels = models.Where(r => !r.IsSimple);

            var rules = ComplexRuleTransfomer.TransformComplexRules(complexRuleModels, simpleRules);

            return rules;
        }

        public TranspilerRule TransformSimpleRule(TranspilerRuleViewModel model)
        {
            return model switch
            {
                DirectTranslationRuleViewModel viewModel => new DirectTranslationRule
                {
                    Name = model.Name,
                    IsStatement = model.IsStatement,
                    InputSymbol = viewModel.InputSymbol,
                    OutputSymbol = viewModel.OutputSymbol
                },
                ExpressionRuleViewModel viewModel => new ExpressionRule
                {
                    Name = model.Name,
                    IsStatement = model.IsStatement,
                    Expression = viewModel.Expression
                },
                _ => throw new ApplicationException("Undefined simple rule")
            };
        }
    }
}
