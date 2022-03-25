namespace LangBuilder.Source.Domain
{
    public class TranspilerModel
    {
        public string Name { get; set; }
        public IEnumerable<TranspilerRule> Rules { get; set; }
        public string GrammarName;
    }
}