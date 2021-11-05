using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using LangBuilder.Source.Domain;

namespace LangBuilder.Source.JsonConverter
{
    public class TranspilerRuleSetJsonConverter : JsonConverter<TranspilerRuleSet>
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeof(TranspilerRuleSet).IsAssignableFrom(typeToConvert);

        public override TranspilerRuleSet? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            (reader.TokenType == JsonTokenType.StartObject).OrThrowJsonException();

            reader.Read();
            (reader.TokenType == JsonTokenType.PropertyName).OrThrowJsonException();

            var propertyName = reader.GetString();
            (propertyName.ToLower() == nameof(TranspilerRuleSet.Rules).ToLower()).OrThrowJsonException();

            reader.Read();
            (reader.TokenType == JsonTokenType.StartArray).OrThrowJsonException();

            var rules = new List<TranspilerRuleViewModel>();

            while (ReadNextRule(ref reader, out var rule))
            {
                rules.Add(rule);
            }

            reader.Read();

            (reader.TokenType == JsonTokenType.EndObject).OrThrowJsonException();

            return new TranspilerRuleSet
            {
                Rules = rules
            };
        }

        private bool ReadNextRule(ref Utf8JsonReader reader, out TranspilerRuleViewModel rule)
        {
            reader.Read();

            if(reader.TokenType == JsonTokenType.EndArray)
            {
                rule = null;
                return false;
            }

            (reader.TokenType == JsonTokenType.StartObject).OrThrowJsonException();

            var name = reader.ReadStringProperty(nameof(TranspilerRuleViewModel.Name));

            reader.Read();

            (reader.TokenType == JsonTokenType.PropertyName).OrThrowJsonException();

            var propertyName = reader.GetString();
            (propertyName.ToLower() == nameof(TranspilerRuleViewModel.Type).ToLower()).OrThrowJsonException();

            reader.Read();

            (reader.TokenType == JsonTokenType.Number).OrThrowJsonException();

            RuleType ruleType = (RuleType) reader.GetInt32();

            rule = ruleType switch
            {
                RuleType.DirectTranslation => ReadDirectTranslationRule(ref reader),
                RuleType.Expression => ReadExpressionRule(ref reader),
                RuleType.Block => ReadBlockRule(ref reader),
                RuleType.RuleSequence => ReadRuleSequenceRule(ref reader),
                RuleType.RuleOptionSequence => ReadRuleOptionSequenceRule(ref reader)
            };

            reader.Read();

            (reader.TokenType == JsonTokenType.EndObject).OrThrowJsonException();

            rule.Name = name;
            rule.Type = ruleType;

            return true;
        }

        private DirectTranslationRuleViewModel ReadDirectTranslationRule(ref Utf8JsonReader reader)
        {
            var inputSymbol = reader.ReadStringProperty(nameof(DirectTranslationRuleViewModel.InputSymbol));

            var outputSymbol = reader.ReadStringProperty(nameof(DirectTranslationRuleViewModel.OutputSymbol));

            return new DirectTranslationRuleViewModel
            {
                InputSymbol = inputSymbol,
                OutputSymbol = outputSymbol
            };
        }

        public ExpressionRuleViewModel ReadExpressionRule(ref Utf8JsonReader reader)
        {
            var expression = reader.ReadStringProperty(nameof(ExpressionRuleViewModel.Expression));

            return new ExpressionRuleViewModel
            {
                Expression = expression
            };
        }

        public BlockRuleViewModel ReadBlockRule(ref Utf8JsonReader reader)
        {
            var blockStart = reader.ReadStringProperty(nameof(BlockRuleViewModel.BlockStart));

            var blockBody = reader.ReadStringProperty(nameof(BlockRuleViewModel.BlockBody));

            var blockEnd = reader.ReadStringProperty(nameof(BlockRuleViewModel.BlockEnd));

            return new BlockRuleViewModel()
            {
                BlockStart = blockStart,
                BlockBody = blockBody,
                BlockEnd = blockEnd
            };
        }

        public RuleSequenceRuleViewModel ReadRuleSequenceRule(ref Utf8JsonReader reader)
        {
            var delimiter = reader.ReadStringProperty(nameof(RuleSequenceRuleViewModel.Delimiter));

            reader.Read();

            (reader.TokenType == JsonTokenType.PropertyName).OrThrowJsonException();

            var propertyName = reader.GetString();
            
            (propertyName.ToLower() == nameof(RuleSequenceRuleViewModel.Rules).ToLower()).OrThrowJsonException();

            reader.Read();

            (reader.TokenType == JsonTokenType.StartArray).OrThrowJsonException();

            var ruleNames = new List<string>();

            reader.Read();

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                (reader.TokenType == JsonTokenType.String).OrThrowJsonException();

                var ruleName = reader.GetString();

                ruleNames.Add(ruleName);

                reader.Read();
            }

            return new RuleSequenceRuleViewModel()
            {
                Delimiter = delimiter,
                Rules = ruleNames
            };
        }

        public RuleOptionSequenceRuleViewModel ReadRuleOptionSequenceRule(ref Utf8JsonReader reader)
        {
            reader.Read();

            (reader.TokenType == JsonTokenType.PropertyName).OrThrowJsonException();

            var propertyName = reader.GetString();

            (propertyName.ToLower() == nameof(RuleOptionSequenceRuleViewModel.Rules).ToLower()).OrThrowJsonException();

            reader.Read();

            (reader.TokenType == JsonTokenType.StartArray).OrThrowJsonException();

            var ruleNames = new List<string>();

            reader.Read();

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                (reader.TokenType == JsonTokenType.String).OrThrowJsonException();

                var ruleName = reader.GetString();

                ruleNames.Add(ruleName);

                reader.Read();
            }

            return new RuleOptionSequenceRuleViewModel()
            {
                Rules = ruleNames
            };
        }

        public override void Write(Utf8JsonWriter writer, TranspilerRuleSet value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}