using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineShop.UI.Helpers
{
    public sealed class HeaderCollection : Dictionary<string, HeaderValue>
    {
        public void CopyTo(HttpContentHeaders httpHeaders)
        {
            foreach (var (key, value) in this.Where(x => !x.Value.IsNull))
            {
                httpHeaders.Add(key, value.ToString());
            }
        }

        public void CopyTo(HttpRequestHeaders httpHeaders)
        {
            foreach (var (key, value) in this.Where(x => !x.Value.IsNull))
            {
                httpHeaders.Add(key, value.ToString());
            }
        }
    }

    public static class ByteArrayExt
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToBase64String(this byte[] self) => Convert.ToBase64String(self);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToBase64Icon(this byte[] self) => "data:image/png;base64, " + Convert.ToBase64String(self);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToUtf8String(this byte[] self) => Encoding.UTF8.GetString(self);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToAsciiString(this byte[] self) => Encoding.ASCII.GetString(self);
    }

    public static class HeaderDictionaryExt
    {
        /// <summary>
        /// Retrieves header value and passes it to validateAction
        /// </summary>
        /// <param name="self">IHeaderDictionary</param>
        /// <param name="headerKey">The key whose value to get</param>
        /// <param name="validateAction">Action to call for header value</param>
        /// <param name="onlyFirstValue">Whether to check only first value with given key or all</param>
        public static void ValidateHeaderValue(this IHeaderDictionary self, string headerKey, Action<string?> validateAction, bool onlyFirstValue = false)
        {
            if (!self.TryGetValue(headerKey, out var headerValue))
                validateAction(null);

            for (var i = 0; i < headerValue.Count; i++)
            {
                var item = headerValue[i];
                validateAction(item);
                if (onlyFirstValue)
                    break;
            }
        }

        public static string? GetHeaderValue(this IHeaderDictionary self, string headerKey, bool failIfDuplicatesExists = true)
        {
            if (!self.TryGetValue(headerKey, out var values) || values.Count == 0)
                return null;

            if (failIfDuplicatesExists && values.Count > 1)
                throw new ArgumentException($"Duplicate header values '{headerKey}'", headerKey);

            return values[0].Trim();
        }

        public static string GetRequiredHeaderValue(this IHeaderDictionary self, string headerKey, bool canBeEmpty = false, bool failIfDuplicatesExists = true)
        {
            if (!self.TryGetValue(headerKey, out var values) || values.Count == 0)
                throw new ArgumentException($"Required header not found '{headerKey}'", headerKey);

            if (failIfDuplicatesExists && values.Count > 1)
                throw new ArgumentException($"Duplicate header values '{headerKey}'", headerKey);

            var value = values[0].Trim();

            if (!canBeEmpty && string.IsNullOrEmpty(value))
                throw new ArgumentException($"Required header is empty '{headerKey}'", headerKey);
            return value;
        }

        public static string GetRequiredHeaderValue(this HttpHeaders self, string headerKey, bool canBeEmpty = false, bool failIfDuplicatesExists = true)
        {
            if (!self.TryGetValues(headerKey, out var values))
                throw new ArgumentException($"Required header not found '{headerKey}'", headerKey);

            var x = values.ToArray();

            if (failIfDuplicatesExists && x.Length > 1)
                throw new ArgumentException($"Duplicate header values '{headerKey}'", headerKey);

            var value = x[0].Trim();

            if (!canBeEmpty && string.IsNullOrEmpty(value))
                throw new ArgumentException($"Required header is empty '{headerKey}'", headerKey);
            return value;
        }
    }
}