using LangBuilder.Source.Extensions;

namespace LangBuilder.Source.Domain
{
    public class RuleSequenceRuleViewModel : TranspilerRuleViewModel
    {
        public override string Type { get; } = "RuleSequence";
        public string Delimiter { get; set; }
        public IEnumerable<string> Rules { get; set; }
    }

    public class RuleSequenceRule : TranspilerRule
    {
        public string Delimiter { get; set; }
        public IEnumerable<TranspilerRule> Rules { get; set; }

        public override string GrammarRule => string.Join(" ", Rules.Select(r => r.Name));
        public override string RuleBody => $"return {string.Join($" + \"{Delimiter}\" + ", Rules.Select(r => $"Visit(context.{r.Name.ToLowercaseFirstLetter()}())"))};";
    }
}
