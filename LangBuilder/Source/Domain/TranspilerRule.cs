namespace LangBuilder.Source.Domain
{
    public class TranspilerRuleViewModel
    {
        public string Name { get; set; }
        public RuleType Type { get; set; }

    }

    public abstract class TranspilerRule
    {
        public string Name { get; set; }
        public abstract string GrammarRule { get; }
        public abstract string RuleBody { get; }
    }
}
