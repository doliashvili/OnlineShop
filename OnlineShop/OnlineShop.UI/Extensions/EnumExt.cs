using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OnlineShop.UI.Extensions
{
    public static class EnumExt
    {
        private static class EnumNameValueCache<TEnum> where TEnum : struct, Enum
        {
            internal static readonly Dictionary<TEnum, string>? ValueToName;
            internal static readonly Dictionary<string, TEnum>? NameToValue;

            static EnumNameValueCache()
            {
                var ty = typeof(TEnum);

                try
                {
                    ValueToName = Enum.GetValues<TEnum>().ToDictionary(x => x, GetName);
                    NameToValue = ValueToName.ToDictionary(x => x.Value, x => x.Key);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                string GetName(TEnum input)
                {
                    var name = input.ToString();
                    return ty.GetField(name)?.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? name;
                }
            }
        }

        private static readonly ConcurrentDictionary<Enum, string> EnumToStringCache = new();

        public static string? ToEnumStringOrNull(this Enum? self) => self?.ToEnumString();

        /// <summary>
        /// Converts Enum to string using EnumMemberAttribute
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToEnumString(this Enum self)
        {
            var enumType = self.GetType();
            var isFlags = enumType.IsDefined(typeof(FlagsAttribute), false);

            return isFlags
                ? string.Join(", ",
                    Enum.GetValues(enumType)
                        .Cast<Enum>()
                        .Where(self.HasFlag)
                        .Select(GetName))
                : GetName(self);

            static string GetName(Enum enumValue) => EnumToStringCache.GetOrAdd(enumValue, x =>
            {
                var name = x.ToString();

                var attr = x.GetType().GetField(name)?.GetCustomAttribute<EnumMemberAttribute>(false);
                return attr is null ? name : attr.Value ?? name;
            });
        }

        //private static string InternalGetEnumStringValue(Enum enumValue)
        //{
        //    return _enumToStringCache.GetOrAdd(enumValue, x =>
        //    {
        //        var name = x.ToString();

        //        var attr = x.GetType().GetField(name)?.GetCustomAttribute<EnumMemberAttribute>(false);
        //        return attr is null ? name : attr.Value ?? name;
        //    });
        //}

        //public static T ToEnum<T>(this string self) where T : Enum
        //{
        //    var enumType = typeof(T);
        //    bool isFlags = enumType.IsDefined(typeof(FlagsAttribute), false);

        //    if (isFlags)
        //    {
        //        ulong result = 0;
        //        string[] flagValues = self.Split(", ", StringSplitOptions.RemoveEmptyEntries);

        //        for (var i = 0; i < flagValues.Length; i++)
        //        {
        //            result |= GetEnumValueAndRawValue<T>(flagValues[i]).rawValue;
        //        }
        //        return (T)Enum.ToObject(enumType, result);
        //    }

        //    return GetEnumValueAndRawValue<T>(self).enumValue;
        //}

        //private static (T enumValue, ulong rawValue) GetEnumValueAndRawValue<T>(string enumStringValue) where T : Enum
        //{
        //    var enumInfo = _stringToEnumgCache.GetOrAdd((typeof(T), enumStringValue), x =>
        //    {
        //        string[] array = Enum.GetNames(x.enumType);

        //        for (var i = 0; i < array.Length; i++)
        //        {
        //            var name = array[i];
        //            var attr = x.enumType.GetField(name)?.GetCustomAttribute<EnumMemberAttribute>(false);
        //            if (attr is null)
        //            {
        //                if (name == x.value)
        //                    return CreateResult(x.enumType, name);
        //            }
        //            else
        //            if (attr.Value == x.value)
        //            {
        //                return CreateResult(x.enumType, name);
        //            }
        //        }

        //        throw new FormatException($"Cannot convert string value '{x.value}' to enum of type {x.enumType.Name}");
        //    });

        //    return ((T)enumInfo.enumValue, enumInfo.rawValue);

        //    static (T enumValue, ulong rawValue) CreateResult(Type enumType, string name)
        //    {
        //        var value = (T)Enum.Parse(enumType, name);
        //        var rawValue = GetEnumRawValue(Type.GetTypeCode(enumType), value);
        //        return (value, rawValue);
        //    }
        //}

        //private static ulong GetEnumRawValue(TypeCode enumTypeCode, object value)
        //{
        //    return enumTypeCode switch
        //    {
        //        TypeCode.Byte => (byte)value,
        //        TypeCode.Int32 => (ulong)(int)value,
        //        TypeCode.Int16 => (ulong)(short)value,
        //        TypeCode.Int64 => (ulong)(long)value,

        //        TypeCode.SByte => (ulong)(sbyte)value,
        //        TypeCode.UInt32 => (uint)value,
        //        TypeCode.UInt16 => (ushort)value,
        //        TypeCode.UInt64 => (ulong)value,
        //        _ => throw new NotImplementedException(),
        //    };
        //}

        public static string ToEnumString<TEnum>(this TEnum self)
            where TEnum : struct, Enum
        {
            var cache = EnumNameValueCache<TEnum>.ValueToName;
            if (cache is null)
                return self.ToString();

            return typeof(TEnum).IsDefined(typeof(FlagsAttribute), false)
                ? string.Join(", ",
                    Enum.GetValues<TEnum>()
                        .Where(x => self.HasFlag(x))
                        .Select(x => GetName(cache, x)))
                : GetName(cache, self);

            static string GetName(Dictionary<TEnum, string> cache, TEnum input) =>
                cache.TryGetValue(input, out var name)
                    ? name
                    : throw new ArgumentOutOfRangeException($"Invalid input value '{input}' for enum of type {typeof(TEnum)}");
        }

        public static TEnum ToEnum<TEnum>(this string self)
            where TEnum : struct, Enum, IConvertible
        {
            if (string.IsNullOrWhiteSpace(self))
            {
                throw new FormatException("Input is empty");
            }

            var cache = EnumNameValueCache<TEnum>.NameToValue;

            if (cache is null)
                return Enum.Parse<TEnum>(self);

            var enumType = typeof(TEnum);

            return enumType.IsDefined(typeof(FlagsAttribute), false)
                ? (TEnum)Enum.ToObject(enumType,
                    self.Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(0L, (acc, value) => acc | GetValue(cache, value.Trim()).ToInt64(null)))
                : GetValue(cache, self);

            static TEnum GetValue(Dictionary<string, TEnum> cache, string input) =>
                cache.TryGetValue(input, out var value)
                    ? value
                    : throw new FormatException($"Invalid input value '{input}' for enum of type {typeof(TEnum)}");
        }

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="self">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
        public static T? GetAttributeOfType<T>(this Enum self) where T : System.Attribute
        {
            var type = self.GetType();
            var memInfo = type.GetMember(self.ToString());
            if (memInfo.Length == 0)
                return null;

            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }
    }
}