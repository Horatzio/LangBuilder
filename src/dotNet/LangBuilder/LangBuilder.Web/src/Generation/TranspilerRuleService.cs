using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LangBuilder.Source.Domain;

namespace LangBuilder.Source.Service
{
    public class TranspilerRuleService
    {
        public async Task<IEnumerable<TranspilerRule>> ProcessRules(TranspilerRuleSet ruleSet)
        {
            var models = ruleSet.Rules;

            var simpleRuleModels = models.Where(r => r.Type.IsSimple());

            var simpleRules = simpleRuleModels
                .Select(TransformSimpleRule);

            var complexRuleModels = models.Where(r => !r.Type.IsSimple());

            var rules = ComplexRuleTransfomer.TransformComplexRules(complexRuleModels, simpleRules);

            return rules;
        }

        public TranspilerRule TransformSimpleRule(TranspilerRuleViewModel model)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return model switch
            {
                DirectTranslationRuleViewModel viewModel => new DirectTranslationRule
                {
                    Name = model.Name,
                    InputSymbol = viewModel.InputSymbol,
                    OutputSymbol = viewModel.OutputSymbol
                },
                ExpressionRuleViewModel viewModel => new ExpressionRule
                {
                    Name = model.Name,
                    Expression = viewModel.Expression
                },
                _ => throw new ApplicationException("Undefined simple rule")
            };
        }
    }
}
