using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LangBuilder.Source.Domain;
using LangBuilder.Source.Models;

namespace LangBuilder.Source.Service
{
    public class TranspilerRuleService
    {
        public async Task<IEnumerable<TranspilerRule>> ProcessRules(IEnumerable<TranspilerRuleViewModel> models)
        {
            var simpleRules = models.Where(r => r.Type.IsSimple())
                .Select(TransformSimpleRule);

            var complexRules = models.Select(r => TransformComplexRule(r, simpleRules));

            return simpleRules.Concat(complexRules);
        }

        public TranspilerRule TransformSimpleRule(TranspilerRuleViewModel model)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return model.Type switch
            {
                RuleType.DirectTranslation => new DirectTranslationRule(
                    model.Properties[nameof(DirectTranslationRule.InputSymbol)] as string,
                    model.Properties[nameof(DirectTranslationRule.OutputSymbol)] as string) {Name = model.Name},
                RuleType.Expression => new ExpressionRule(model.Properties[nameof(ExpressionRule.Expression)] as string)
                {
                    Name = model.Name
                },
                _ => throw new ApplicationException("Undefined simple rule")
            };
        }

        public TranspilerRule TransformComplexRule(TranspilerRuleViewModel model, IEnumerable<TranspilerRule> simpleRules)
        {
            return model.Type switch
            {
                RuleType.Block => new BlockRule(
                    simpleRules.First(r => r.Name == model.Properties[nameof(BlockRule.BlockStart)] as string),
                    simpleRules.First(r => r.Name == model.Properties[nameof(BlockRule.BlockStart)] as string),
                    simpleRules.First(r => r.Name == model.Properties[nameof(BlockRule.BlockStart)] as string)
                ),
                _ => throw new ApplicationException("Undefined complex rule")
            };
        }
    }
}
