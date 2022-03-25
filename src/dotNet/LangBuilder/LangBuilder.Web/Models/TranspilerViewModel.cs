using LangBuilder.Source.Domain;

namespace LangBuilder.Source.Models
{
    public class TranspilerViewModel
    {
        public string Name { get; set; }
        public IEnumerable<TranspilerRuleViewModel> Rules { get; set; }
    }
}
