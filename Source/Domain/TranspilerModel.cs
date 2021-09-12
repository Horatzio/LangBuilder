using System.Collections.Generic;

namespace LangBuilder.Source.Domain
{
    public class TranspilerModel
    {
        public string GrammarName { get; set; }
        public string Name { get; set; }

        public IEnumerable<TranspilerRule> Rules { get; set; }
    }
}
