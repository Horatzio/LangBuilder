using JsonSubTypes;
using Newtonsoft.Json;

namespace LangBuilder.Source.Domain
{

    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(BlockRuleViewModel), RuleType.Block)]
    [JsonSubtypes.KnownSubType(typeof(DirectTranslationRule), RuleType.DirectTranslation)]
    [JsonSubtypes.KnownSubType(typeof(ExpressionRuleViewModel), RuleType.Expression)]
    [JsonSubtypes.KnownSubType(typeof(RuleOptionSequenceRuleViewModel), RuleType.RuleOptionSequence)]
    [JsonSubtypes.KnownSubType(typeof(RuleSequenceRuleViewModel), RuleType.RuleSequence)]
    public class TranspilerRuleViewModel
    {
        public string Name { get; set; }
        public virtual RuleType Type { get; }
    }

    public abstract class TranspilerRule
    {
        public string Name { get; set; }
        public abstract string GrammarRule { get; }
        public abstract string RuleBody { get; }
    }
}
