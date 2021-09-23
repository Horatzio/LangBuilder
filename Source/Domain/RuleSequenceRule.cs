using System.Linq;

namespace LangBuilder.Source.Domain
{
    public class RuleSequenceRule : TranspilerRule
    {
        public override string GrammarRule => "statement+";
        public override string RuleBody => $"return \"Testing\"";
    }
}
