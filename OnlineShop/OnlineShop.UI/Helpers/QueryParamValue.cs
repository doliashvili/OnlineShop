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
        internal readonly string? Value;

        internal readonly IEnumerable<QueryParamValue>? Values;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private QueryParamValue(string? value) => Value = value;

        private QueryParamValue(IEnumerable<QueryParamValue>? values) => Values = values;

        public override string ToString() => Value ?? string.Empty;

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

        public static implicit operator QueryParamValue(Guid? value) => new(value?.ToString());

        public static implicit operator QueryParamValue(Enum? value) => new(value?.ToEnumStringOrNull());

        //public static implicit operator QueryParamValue(Array value) => new QueryParamValue(GetString(value));

        public static implicit operator QueryParamValue(List<string?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<int?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<uint?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<long?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<ulong?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<float?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<byte?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<sbyte?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<char?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<decimal?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<double?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<bool?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<short?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<ushort?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<DateTime?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<Guid?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(List<Enum?>? value) => new(value?.ConvertAll(a => (QueryParamValue)a));

        public static implicit operator QueryParamValue(Dictionary<string, string> value) => new(GetKeyValueString(value));

        private static string GetKeyValueString(Dictionary<string, string> dictionary) =>
            string.Join("&", dictionary.Select(o => $"{o.Key}={o.Value}"));
    }
}