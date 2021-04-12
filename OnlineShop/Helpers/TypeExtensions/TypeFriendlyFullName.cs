using System;
using System.Linq;

namespace Helpers.TypeExtensions
{
    public static class TypeFriendlyFullName
    {
        public static string GetFriendlyFullNameForType(this Type type)
        {
            if (type.IsArray)
            {
                var rank = type.GetArrayRank();
                var commas = rank > 1
                    ? new string(',', rank - 1)
                    : "";
                return GetFriendlyFullNameForType(type.GetElementType()) + $"[{commas}]";
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return type.GetGenericArguments()[0].GetFriendlyFullNameForType() + "?";
            
            if (type.IsGenericType)
                return type.FullName?.Split('`')[0] + "<" + string.Join(", ",
                    type.GetGenericArguments().Select(GetFriendlyFullNameForType).ToArray()) + ">";

            return type.FullName;
        }
    }
}