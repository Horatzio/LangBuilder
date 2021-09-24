using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LangBuilder.Source.Domain
{
    public class RuleSet
    {
        public IEnumerable<TranspilerRule> Rules { get; set; }
    }

    public class RuleSetConverter : JsonConverter<RuleSet>
    {
        public override RuleSet Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            if (propertyName.ToLower() != nameof(TranspilerRule.Type))
            {
                throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, RuleSet value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
