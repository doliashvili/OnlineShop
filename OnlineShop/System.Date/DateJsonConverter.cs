using System.Text.Json;
using System.Text.Json.Serialization;

namespace System
{
    public sealed class DateJsonConverter : JsonConverter<Date>
    {
        public override Date Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new InvalidOperationException($"Invalid operation. Expected 'string'. Actual - {reader.TokenType}");
            }

            var str = reader.GetString();
            return Date.Parse(str!);
        }

        public override void Write(Utf8JsonWriter writer, Date value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}