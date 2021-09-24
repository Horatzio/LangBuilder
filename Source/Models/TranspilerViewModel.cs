using System.Collections.Generic;

namespace LangBuilder.Source.Models
{
    public class TranspilerViewModel
    {
        public string GrammarName { get; set; }
        public string Name { get; set; }
        public IEnumerable<TranspilerRuleViewModel> Rules { get; set; }
    }
}
