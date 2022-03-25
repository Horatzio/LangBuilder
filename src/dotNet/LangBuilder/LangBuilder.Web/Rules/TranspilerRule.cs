using JsonSubTypes;
using Newtonsoft.Json;

namespace LangBuilder.Source.Domain
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(BlockRuleViewModel), "Block")]
    [JsonSubtypes.KnownSubType(typeof(DirectTranslationRuleViewModel), "DirectTranslation")]
    [JsonSubtypes.KnownSubType(typeof(ExpressionRuleViewModel), "Expression")]
    [JsonSubtypes.KnownSubType(typeof(RuleOptionSequenceRuleViewModel), "RuleOptionSequence")]
    [JsonSubtypes.KnownSubType(typeof(RuleSequenceRuleViewModel), "RuleSequence")]
    public class TranspilerRuleViewModel
    {
        public virtual string Type { get; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual bool IsSimple { get; } = false;

        public bool IsStatement { get; set; } = false;

    }

    public abstract class TranspilerRule
    {
        public string Name { get; set; }
        public bool IsStatement { get; set; } = false;
        public abstract string GrammarRule { get; }
        public abstract string RuleBody { get; }
    }
}
