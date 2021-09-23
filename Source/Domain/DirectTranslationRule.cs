namespace LangBuilder.Source.Domain
{
    public class DirectTranslationRule : TranspilerRule
    {
        private readonly string _inputSymbol;
        private readonly string _outputSymbol;

        public DirectTranslationRule(string inputSymbol, string outputSymbol)
        {
            _inputSymbol = inputSymbol;
            _outputSymbol = outputSymbol;
        }

        public override string GrammarRule => $"'{_inputSymbol}'";
        public override string RuleBody => $"return {_outputSymbol};";
    }
}
