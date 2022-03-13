namespace LangBuilder.Source.Domain
{
    public class ExpressionRuleViewModel : TranspilerRuleViewModel
    {
        public string Expression { get; set; }
        public override RuleType Type => RuleType.Expression;
    }

    public class ExpressionRule : TranspilerRule
    {
        public string Expression { get; set; }

        public override string GrammarRule => $"'{Expression}'";
        public override string RuleBody => "return context.GetText();";
    }
}
