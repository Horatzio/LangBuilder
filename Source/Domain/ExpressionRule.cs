namespace LangBuilder.Source.Domain
{
    public class ExpressionRule : TranspilerRule
    {
        internal readonly string Expression;

        public ExpressionRule(string expression)
        {
            Expression = expression;
        }

        public override string GrammarRule => $"{Expression}";
        public override string RuleBody => "return context.GetText();";
    }
}
