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
            return models.Select(TransformRule);
        }

        public TranspilerRule TransformRule(TranspilerRuleViewModel model)
        {
            return new DirectTranslationRule("a", "b");
        }

    }
}
