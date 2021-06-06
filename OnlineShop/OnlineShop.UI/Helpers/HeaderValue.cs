using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using OnlineShop.UI.Extensions;

namespace OnlineShop.UI.Helpers
{
    public sealed class HeaderValue
    {
        private readonly string? _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private HeaderValue(string? value) => _value = value;

        public bool IsNull => _value is null;

        //public override string ToString() => _value is null ? string.Empty : EncodeHeaderString(_value);
        public override string ToString() => _value ?? string.Empty;

        public static implicit operator HeaderValue(bool? value) => new(value.HasValue ? value.Value ? "true" : "false" : null);

        public static implicit operator HeaderValue(byte? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator HeaderValue(sbyte? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator HeaderValue(short? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator HeaderValue(ushort? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator HeaderValue(int? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator HeaderValue(uint? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator HeaderValue(long? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator HeaderValue(ulong? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator HeaderValue(decimal? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator HeaderValue(float? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator HeaderValue(double? value) => new(value?.ToString(CultureInfo.InvariantCulture));

        public static implicit operator HeaderValue(string? value) => new(value);

        public static implicit operator HeaderValue(DateTime? value) => new(value?.ToString("s"));

        //  public static implicit operator HeaderValue(Date? value) => new(value?.ToString());

        public static implicit operator HeaderValue(Guid? value) => new(value?.ToString());

        public static implicit operator HeaderValue(Enum? value) => new(value?.ToEnumStringOrNull());

        public static implicit operator HeaderValue(byte[]? value) => new(value?.ToBase64String());

        //private static string EncodeHeaderString(string input)
        //{
        //    StringBuilder? sb = null;

        //    for (var i = 0; i < input.Length; i++)
        //    {
        //        var ch = input[i];

        //        if ((ch < 32 && ch != 9) || ch == 127)
        //        {
        //            sb ??= new StringBuilder();
        //            sb.Append($"%{(int)ch:x2}");
        //        }
        //    }

        //    return sb is null ? input : sb.ToString();
        //}
    }
}