namespace LangBuilder.Source.Domain
{
    public class TranspilerModel
    {
        public string GrammarName { get; set; }
        public string Name { get; set; }

        public RuleSet rules { get; set; }
    }
}