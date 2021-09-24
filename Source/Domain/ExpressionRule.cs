namespace LangBuilder.Source.Domain
{
    public class ExpressionRule : TranspilerRule
    {
        private readonly string _expression;

        public ExpressionRule(string expression)
        {
            _expression = expression;
        }

        public override string GrammarRule => $"{_expression}";
        public override string RuleBody => "return context.GetText();";
    }
}
