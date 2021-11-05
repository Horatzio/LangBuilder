using LangBuilder.Source.Domain;

namespace LangBuilder.Source.Models
{
    public class TranspilerViewModel
    {
        public string GrammarName { get; set; }
        public string Name { get; set; }
        public TranspilerRuleSet RuleSet { get; set; }
    }
}
