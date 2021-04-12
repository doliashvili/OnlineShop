using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace OnlineShop.Domain.Extensions
{
    public static class JsonSerializerExt
    {
        public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = GetDefaultJsonSerializerOptions();
        public static readonly JsonSerializerOptions CaseInsensitiveJsonSerializerOptions = GetCaseInsensitiveJsonSerializerOptions();

        public static string AsJson<TValue>(this TValue value)
        {
            return JsonSerializer.Serialize(value, DefaultJsonSerializerOptions);
        }

        public static string? AsJsonOrNull<TValue>(this TValue? value)
        {
            return value is null ? null : AsJson(value);
        }

        public static string AsJson(this object value, Type inputType)
        {
            return JsonSerializer.Serialize(value, inputType, DefaultJsonSerializerOptions);
        }

        [return: NotNull]
        public static T FromJson<T>(this string json, bool ignoreCase = false)  
        {
            return JsonSerializer.Deserialize<T>(json, ignoreCase ? CaseInsensitiveJsonSerializerOptions : DefaultJsonSerializerOptions) ?? throw new JsonException("Cannot deserialize json");
        }

        public static object FromJson<T>(this string json, Type returnType)
        {
            return JsonSerializer.Deserialize(json, returnType, DefaultJsonSerializerOptions) ?? throw new JsonException("Cannot deserialize json");
        }

        public static void SetDefaultJsonSerializerOptions(this JsonSerializerOptions options)
        {
            options.IgnoreNullValues = true;
            options.Converters.Add(new DateJsonConverter());
            options.Converters.Add(new JsonStringEnumMemberConverter());
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            //options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        }

        private static JsonSerializerOptions GetDefaultJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions();
            SetDefaultJsonSerializerOptions(options);
            return options;
        }

        private static JsonSerializerOptions GetCaseInsensitiveJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            SetDefaultJsonSerializerOptions(options);
            return options;
        }
    }
}