namespace LangBuilder.Source.Domain
{
    public class DirectTranslationRule : TranspilerRule
    {
        internal readonly string InputSymbol;
        internal readonly string OutputSymbol;

        public DirectTranslationRule(string inputSymbol, string outputSymbol)
        {
            InputSymbol = inputSymbol;
            OutputSymbol = outputSymbol;
        }

        public override string GrammarRule => $"'{InputSymbol}'";
        public override string RuleBody => $"return {OutputSymbol};";
    }
}
