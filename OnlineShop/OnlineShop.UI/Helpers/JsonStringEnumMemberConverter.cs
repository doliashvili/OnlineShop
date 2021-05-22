using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using OnlineShop.UI.Extensions;

namespace OnlineShop.UI.Helpers
{
    /// <summary>
    /// Converter to convert enums to and from strings.
    /// </summary>
    public sealed class JsonStringEnumMemberConverter : JsonConverterFactory
    {
        /// <summary>
        /// Constructor. Creates the <see cref="JsonStringEnumConverter"/> with the
        /// default naming policy and allows integer values.
        /// </summary>
        public JsonStringEnumMemberConverter()
        {
            // An empty constructor is needed for construction via attributes
        }

        /// <inheritdoc />
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        /// <inheritdoc />
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(EnumConverter<>).MakeGenericType(typeToConvert),
                BindingFlags.Instance | BindingFlags.Public,
                null,
                null,
                null)!;

            return converter;
        }
    }

    internal class EnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var token = reader.TokenType;

            if (token == JsonTokenType.String)
            {
                var enumString = reader.GetString();
                if (enumString is null)
                    return default;

                try
                {
                    return enumString.ToEnum<T>();
                }
                catch
                {
                    throw new JsonException();
                }
            }
            //$"Unable to deserialize type {typeToConvert}"
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToEnumString());
        }
    }
}