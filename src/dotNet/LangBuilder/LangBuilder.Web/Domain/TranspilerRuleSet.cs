using System.Text.Json.Serialization;
using LangBuilder.Source.JsonConverter;

namespace LangBuilder.Source.Domain
{
    [JsonConverter(typeof(TranspilerRuleSetJsonConverter))]
    public class TranspilerRuleSet
    {
        public IEnumerable<TranspilerRuleViewModel> Rules { get; set; }
    }
}
