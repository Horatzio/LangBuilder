using System.Collections.Generic;
using LangBuilder.Source.Domain;

namespace LangBuilder.Source.Models
{
    public class TranspilerRuleViewModel
    {
        public string Name { get; set; }
        public RuleType Type { get; set; }

        public Dictionary<string, object> Properties;
    }
}
