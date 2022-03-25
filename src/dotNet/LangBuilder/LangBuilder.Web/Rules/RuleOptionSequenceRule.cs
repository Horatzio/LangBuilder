namespace LangBuilder.Source.Domain
{
    public class RuleOptionSequenceRuleViewModel : TranspilerRuleViewModel 
    {
        public override string Type { get; } = "RuleOptionSequence";
        public IEnumerable<string> Rules { get; set; }
    }

    public class RuleOptionSequenceRule : TranspilerRule
    {
        public IEnumerable<TranspilerRule> Rules { get; set; }

        public override string GrammarRule => string.Join(" | ", Rules.Select(r => r.Name));
        public override string RuleBody => $"return VisitChildren(context);";
    }
}
