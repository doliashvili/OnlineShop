using System;

namespace OnlineShop.Infrastructure.CommonSql
{
    public static class StringExt
    {
        public static string MaskDbConnectionString(this string self)
        {
            var chunks = self.Split(';', StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < chunks.Length; i++)
            {
                var tokens = chunks[i].Split('=');
                var token = tokens[0];
                if (token.StartsWith("user", StringComparison.OrdinalIgnoreCase) || token.StartsWith("password", StringComparison.OrdinalIgnoreCase))
                {
                    tokens[1] = "********";
                }

                chunks[i] = token + "=" + tokens[1] + ";";
            }

            return string.Concat(chunks);
        }
    }
}