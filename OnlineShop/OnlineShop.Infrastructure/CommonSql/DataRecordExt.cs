using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.SqlClient;
using OnlineShop.Domain.Extensions;

namespace OnlineShop.Infrastructure.CommonSql
{
    public static class DataRecordExt
    {
        private static TValue? AsNullable<TValue>(this SqlDataReader self, int idx, Func<object, TValue> convert)
            where TValue : struct =>
            self[idx] switch
            {
                DBNull => default(TValue?),
                var value => convert(value)
            };

        #region String

        public static string? AsStringOrNull(this SqlDataReader self, string name) =>
            AsStringOrNull(self, self.GetOrdinal(name));

        public static string? AsStringOrNull(this SqlDataReader self, int idx) => self[idx] switch
        {
            DBNull => null,
            var value => value.ToString()
        };

        public static string AsString(this SqlDataReader self, string name) =>
            AsString(self, self.GetOrdinal(name));

#pragma warning disable CS8603 // Possible null reference return.

        public static string AsString(this SqlDataReader self, int idx) => self[idx] switch
        {
            DBNull => string.Empty,
            var value => value.ToString(),
        };

#pragma warning restore CS8603 // Possible null reference return.

        #endregion String

        #region Boolean

        public static bool AsBoolean(this SqlDataReader self, string name) => AsBoolean(self, self.GetOrdinal(name));

        public static bool? AsBooleanOrNull(this SqlDataReader self, string name) =>
            AsBooleanOrNull(self, self.GetOrdinal(name));

        public static bool AsBoolean(this SqlDataReader self, int idx) => Convert.ToBoolean(self[idx]);

        public static bool? AsBooleanOrNull(this SqlDataReader self, int idx) => self.AsNullable(idx, Convert.ToBoolean);

        #endregion Boolean

        #region Byte

        public static byte AsByte(this SqlDataReader self, string name) => AsByte(self, self.GetOrdinal(name));

        public static byte? AsByteOrNull(this SqlDataReader self, string name) =>
            AsByteOrNull(self, self.GetOrdinal(name));

        public static byte AsByte(this SqlDataReader self, int idx) => Convert.ToByte(self[idx]);

        public static byte? AsByteOrNull(this SqlDataReader self, int idx) => self.AsNullable(idx, Convert.ToByte);

        #endregion Byte

        #region Int16

        public static short AsInt16(this SqlDataReader self, string name) => AsInt16(self, self.GetOrdinal(name));

        public static short? AsInt16OrNull(this SqlDataReader self, string name) =>
            AsInt16OrNull(self, self.GetOrdinal(name));

        public static short AsInt16(this SqlDataReader self, int idx) => Convert.ToInt16(self[idx]);

        public static short? AsInt16OrNull(this SqlDataReader self, int idx) => self.AsNullable(idx, Convert.ToInt16);

        #endregion Int16

        #region Int32

        public static int AsInt32(this SqlDataReader self, string name) => AsInt32(self, self.GetOrdinal(name));

        public static int? AsInt32OrNull(this SqlDataReader self, string name) =>
            AsInt32OrNull(self, self.GetOrdinal(name));

        public static int AsInt32(this SqlDataReader self, int idx) => Convert.ToInt32(self[idx]);

        public static int? AsInt32OrNull(this SqlDataReader self, int idx) => self.AsNullable(idx, Convert.ToInt32);

        #endregion Int32

        #region Int64

        public static long AsInt64(this SqlDataReader self, string name) => AsInt64(self, self.GetOrdinal(name));

        public static long? AsInt64OrNull(this SqlDataReader self, string name) =>
            AsInt64OrNull(self, self.GetOrdinal(name));

        public static long AsInt64(this SqlDataReader self, int idx) => Convert.ToInt64(self[idx]);

        public static long? AsInt64OrNull(this SqlDataReader self, int idx) => self.AsNullable(idx, Convert.ToInt64);

        #endregion Int64

        #region Decimal

        public static decimal AsDecimal(this SqlDataReader self, string name) => AsDecimal(self, self.GetOrdinal(name));

        public static decimal? AsDecimalOrNull(this SqlDataReader self, string name) =>
            AsDecimalOrNull(self, self.GetOrdinal(name));

        public static decimal AsDecimal(this SqlDataReader self, int idx) => Convert.ToDecimal(self[idx]);

        public static decimal? AsDecimalOrNull(this SqlDataReader self, int idx) =>
            self.AsNullable(idx, Convert.ToDecimal);

        #endregion Decimal

        #region Double

        public static double AsDouble(this SqlDataReader self, string name) => AsDouble(self, self.GetOrdinal(name));

        public static double? AsDoubleOrNull(this SqlDataReader self, string name) =>
            AsDoubleOrNull(self, self.GetOrdinal(name));

        public static double AsDouble(this SqlDataReader self, int idx) => Convert.ToDouble(self[idx]);

        public static double? AsDoubleOrNull(this SqlDataReader self, int idx) => self.AsNullable(idx, Convert.ToDouble);

        #endregion Double

        #region Float

        public static float AsFloat(this SqlDataReader self, string name) => AsFloat(self, self.GetOrdinal(name));

        public static float? AsFloatOrNull(this SqlDataReader self, string name) =>
            AsFloatOrNull(self, self.GetOrdinal(name));

        public static float AsFloat(this SqlDataReader self, int idx) => Convert.ToSingle(self[idx]);

        public static float? AsFloatOrNull(this SqlDataReader self, int idx) => self.AsNullable(idx, Convert.ToSingle);

        #endregion Float

        #region DateTime

        public static DateTime AsDateTime(this SqlDataReader self, string name) =>
            AsDateTime(self, self.GetOrdinal(name));

        public static DateTime? AsDateTimeOrNull(this SqlDataReader self, string name) =>
            AsDateTimeOrNull(self, self.GetOrdinal(name));

        private static DateTime ConvertToLocalDateTime(object input) =>
            new(Convert.ToDateTime(input).Ticks, DateTimeKind.Local);

        public static DateTime AsDateTime(this SqlDataReader self, int idx) => ConvertToLocalDateTime(self[idx]);

        public static DateTime? AsDateTimeOrNull(this SqlDataReader self, int idx) =>
            self.AsNullable(idx, ConvertToLocalDateTime);

        #endregion DateTime

        #region Date

        public static Date AsDate(this SqlDataReader self, string name) => AsDate(self, self.GetOrdinal(name));

        public static Date? AsDateOrNull(this SqlDataReader self, string name) =>
            AsDateOrNull(self, self.GetOrdinal(name));

        private static Date ConvertToDate(object input) => (Date)Convert.ToDateTime(input).Date;

        public static Date AsDate(this SqlDataReader self, int idx) => ConvertToDate(self[idx]);

        public static Date? AsDateOrNull(this SqlDataReader self, int idx) => self.AsNullable(idx, ConvertToDate);

        #endregion Date

        #region TimeSpan

        public static TimeSpan AsTimeSpan(this SqlDataReader self, string name) =>
            AsTimeSpan(self, self.GetOrdinal(name));

        public static TimeSpan? AsTimeSpanOrNull(this SqlDataReader self, string name) =>
            AsTimeSpanOrNull(self, self.GetOrdinal(name));

        private static TimeSpan ConvertToTimeSpan(object input) => (TimeSpan)input;

        public static TimeSpan AsTimeSpan(this SqlDataReader self, int idx) => ConvertToTimeSpan(self[idx]);

        public static TimeSpan? AsTimeSpanOrNull(this SqlDataReader self, int idx) =>
            self.AsNullable(idx, ConvertToTimeSpan);

        #endregion TimeSpan

        #region Bytes

        public static byte[]? AsBytesOrNull(this SqlDataReader self, string name) => AsBytesOrNull(self, self.GetOrdinal(name));

        public static byte[]? AsBytesOrNull(this SqlDataReader self, int idx) => self[idx] switch
        {
            DBNull => null,
            var value => (byte[])value
        };

        public static byte[] AsBytes(this SqlDataReader self, string name) => AsBytes(self, self.GetOrdinal(name));

        public static byte[] AsBytes(this SqlDataReader self, int idx) => (byte[])self[idx];

        #endregion Bytes

        #region Enum

        public static TEnum AsEnum<TEnum>(this SqlDataReader self, string name) where TEnum : struct =>
            AsEnum<TEnum>(self, self.GetOrdinal(name));

        public static TEnum? AsEnumOrNull<TEnum>(this SqlDataReader self, string name) where TEnum : struct =>
            AsEnumOrNull<TEnum>(self, self.GetOrdinal(name));

        public static TEnum AsEnum<TEnum>(this SqlDataReader self, int idx) where TEnum : struct
        {
            var value = self[idx];
            return (TEnum)value;
        }

        public static TEnum? AsEnumOrNull<TEnum>(this SqlDataReader self, int idx) where TEnum : struct
        {
            var value = self[idx];
            if (Convert.IsDBNull(value))
                return null;
            return (TEnum)value;
        }

        public static TEnum AsEnumChar<TEnum>(this SqlDataReader self, string name) where TEnum : struct =>
            AsEnumChar<TEnum>(self, self.GetOrdinal(name));

        public static TEnum? AsEnumCharOrNull<TEnum>(this SqlDataReader self, string name) where TEnum : struct =>
            AsEnumCharOrNull<TEnum>(self, self.GetOrdinal(name));

        public static TEnum AsEnumChar<TEnum>(this SqlDataReader self, int idx) where TEnum : struct
        {
            var value = (object)(int)Convert.ToChar(self[idx]);
            return (TEnum)value;
        }

        public static TEnum? AsEnumCharOrNull<TEnum>(this SqlDataReader self, int idx) where TEnum : struct
        {
            var value = self[idx];
            if (Convert.IsDBNull(value))
                return null;

            var enumValue = (object)(int)Convert.ToChar(value);
            return (TEnum)enumValue;
        }

        public static TEnum AsEnumString<TEnum>(this SqlDataReader self, string name) where TEnum : struct, System.Enum =>
            AsEnumString<TEnum>(self, self.GetOrdinal(name));

        public static TEnum? AsEnumStringOrNull<TEnum>(this SqlDataReader self, string name) where TEnum : struct, System.Enum =>
            AsEnumStringOrNull<TEnum>(self, self.GetOrdinal(name));

        private static TEnum ParseEnum<TEnum>(string input) where TEnum : struct, System.Enum =>
            System.Enum.TryParse<TEnum>(input, true, out var result)
                ? result
                : throw new FormatException($"Cannot convert string value '{input}' to enum {typeof(TEnum)}");

        public static TEnum AsEnumString<TEnum>(this SqlDataReader self, int idx) where TEnum : struct, System.Enum =>
            self.AsStringOrNull(idx) switch
            {
                { } s when !string.IsNullOrWhiteSpace(s) => ParseEnum<TEnum>(s),
                _ => throw new FormatException($"Cannot convert empty string value to enum {typeof(TEnum)}")
            };

        public static TEnum? AsEnumStringOrNull<TEnum>(this SqlDataReader self, int idx) where TEnum : struct, System.Enum =>
            self.AsStringOrNull(idx) switch
            {
                { } s when !string.IsNullOrWhiteSpace(s) => ParseEnum<TEnum>(s),
                _ => null
            };

        #endregion Enum

        #region Guid

        public static Guid AsGuid(this SqlDataReader self, string name) => AsGuid(self, self.GetOrdinal(name));

        public static Guid? AsGuidOrNull(this SqlDataReader self, string name) =>
            AsGuidOrNull(self, self.GetOrdinal(name));

        public static Guid AsGuid(this SqlDataReader self, int idx) => self.GetGuid(idx);

        public static Guid? AsGuidOrNull(this SqlDataReader self, int idx) => self[idx] switch
        {
            DBNull => null,
            Guid value => value,
            _ => null
        };

        #endregion Guid

        #region Json

        [return: MaybeNull]
        public static TResult AsJsonOrNull<TResult>(this SqlDataReader self, string name) where TResult : class =>
            self.AsJsonOrNull<TResult>(self.GetOrdinal(name));

        [return: MaybeNull]
        public static TResult AsJsonOrNull<TResult>(this SqlDataReader self, int idx) where TResult : class =>
            self.AsStringOrNull(idx) switch
            {
                { } s => s.FromJson<TResult>(),
                _ => null
            };

        [return: NotNull]
        public static TResult AsJson<TResult>(this SqlDataReader self, string name) where TResult : class =>
            self.AsJson<TResult>(self.GetOrdinal(name));

        [return: NotNull]
        public static TResult AsJson<TResult>(this SqlDataReader self, int idx) where TResult : class =>
           self.AsStringOrNull(idx) switch
           {
               { } s => s.FromJson<TResult>() ?? throw new ApplicationException("Null value for not null Json"),
               _ => throw new ApplicationException("Null value for not null Json")
           };

        #endregion Json

        #region Any Type

        public static TResult AsType<TResult>(this SqlDataReader self, string name) => AsType<TResult>(self, self.GetOrdinal(name));

        public static TResult? AsTypeOrNull<TResult>(this SqlDataReader self, string name) where TResult : struct => AsTypeOrNull<TResult>(self, self.GetOrdinal(name));

        private static TResult ConvertTo<TResult>(object value) => (TResult)(typeof(TResult).IsEnum
            ? Enum.ToObject(typeof(TResult), value)
            : Convert.ChangeType(value, typeof(TResult)));

        public static TResult AsType<TResult>(this SqlDataReader self, int idx) => ConvertTo<TResult>(self[idx]);

        public static TResult? AsTypeOrNull<TResult>(this SqlDataReader self, int idx) where TResult : struct =>
            self[idx] switch
            {
                DBNull => null,
                var value => ConvertTo<TResult>(value)
            };

        #endregion Any Type
    }
}