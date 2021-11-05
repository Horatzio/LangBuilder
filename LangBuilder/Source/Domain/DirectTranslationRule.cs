namespace LangBuilder.Source.Domain
{
    public class DirectTranslationRuleViewModel : TranspilerRuleViewModel
    {
        public string InputSymbol { get; set; }
        public string OutputSymbol { get; set; }
    }

    public class DirectTranslationRule : TranspilerRule
    {
        public string InputSymbol { get; set; }
        public string OutputSymbol { get; set; }

        public override string GrammarRule => $"'{InputSymbol}'";
        public override string RuleBody => $"return \"{OutputSymbol}\";";
    }
}
