using System.Collections.Generic;
using System.Linq;

namespace LangBuilder.Source.Domain
{
    public class RuleOptionSequenceRuleViewModel : TranspilerRuleViewModel 
    {
        public IEnumerable<string> Rules { get; set; }
        public override RuleType Type => RuleType.RuleOptionSequence;
    }

    public class RuleOptionSequenceRule : TranspilerRule
    {
        public IEnumerable<TranspilerRule> Rules { get; set; }

        public override string GrammarRule => string.Join(" | ", Rules.Select(r => r.Name));
        public override string RuleBody => $"return VisitChildren(context);";
    }
}
