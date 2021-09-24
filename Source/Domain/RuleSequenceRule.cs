using System.Linq;

namespace LangBuilder.Source.Domain
{
    public class RuleSequenceRule : TranspilerRule
    {
        private readonly string _delimiter;
        private readonly TranspilerRule[] _rules;

        public RuleSequenceRule(string delimiter, params TranspilerRule[] rules)
        {
            _delimiter = delimiter;
            _rules = rules;
        }

        public override string GrammarRule => string.Join(" ", _rules.Select(r => r.Name));
        public override string RuleBody => $"return {string.Join(" + ", "context." + _rules.Select(r => r.Name) + "()")}";
    }
}
