using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using OnlineShop.UI.Extensions;

namespace OnlineShop.UI.Helpers
{
    public sealed class QueryParamValue
    {
        private readonly string? _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private QueryParamValue(string? value) => _value = value;

        public override string ToString() => _value ?? string.Empty;

        public bool HasValue => _value is not null;

        public static implicit operator QueryParamValue(bool? value) => new(value.HasValue ? value.Value ? "true" : "false" : null);

        public static implicit operator QueryParamValue(byte? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator QueryParamValue(sbyte? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator QueryParamValue(short? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator QueryParamValue(ushort? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator QueryParamValue(int? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator QueryParamValue(uint? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator QueryParamValue(long? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator QueryParamValue(ulong? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator QueryParamValue(decimal? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator QueryParamValue(float? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator QueryParamValue(double? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator QueryParamValue(string? value) => new(value);

        public static implicit operator QueryParamValue(DateTime? value) => new(value?.ToString("s"));

        // public static implicit operator QueryParamValue(Date? value) => new(value?.ToString());

        public static implicit operator QueryParamValue(Guid? value) => new(value?.ToString());

        public static implicit operator QueryParamValue(Enum? value) => new(value?.ToEnumStringOrNull());

        //public static implicit operator QueryParamValue(Array value) => new QueryParamValue(GetString(value));

        public static implicit operator QueryParamValue(List<string> value) => new(GetString(value));

        public static implicit operator QueryParamValue(List<int> value) => new(GetString(value));

        public static implicit operator QueryParamValue(List<long> value) => new(GetString(value));

        public static implicit operator QueryParamValue(Dictionary<string, string> value) => new(GetKeyValueString(value));

        private static string GetKeyValueString(Dictionary<string, string> dictionary) =>
            string.Join("&", dictionary.Select(o => $"{o.Key}={o.Value}"));

        private static string GetString(IEnumerable arr)
        {
            return string.Join("&", GetArrayItems());

            IEnumerable<string> GetArrayItems()
            {
                foreach (var item in arr)
                {
                    var s = item.ToString();
                    if (s is not null)
                        yield return s;
                }
            }
        }
    }
}