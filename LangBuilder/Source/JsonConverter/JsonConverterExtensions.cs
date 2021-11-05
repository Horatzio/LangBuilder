using System.Text.Json;

namespace LangBuilder.Source.JsonConverter
{
    public static class JsonConverterExtensions
    {
        public static string ReadStringProperty(this ref Utf8JsonReader reader, string propertyName)
        {
            reader.Read();

            (reader.TokenType == JsonTokenType.PropertyName).OrThrowJsonException();

            var property = reader.GetString();

            (property.ToLower() == propertyName.ToLower()).OrThrowJsonException();

            reader.Read();

            (reader.TokenType == JsonTokenType.String).OrThrowJsonException();

            var value = reader.GetString();

            return value;
        }

        public static void OrThrowJsonException(this bool expression)
        {
            if(!expression)
                throw new JsonException();
        }
    }
}
