using System;
using System.Text;

namespace OnlineShop.Infrastructure.CommonSql
{
    public static class StringBuilderExt
    {
        public static StringBuilder AppendWithAnd(this StringBuilder self, string item)
        {
            if (self.Length > 0)
                self.Append(" AND ");

            self.Append(item);
            return self;
        }

        public static StringBuilder AppendWhereIfHaveCondition(this StringBuilder self, string sql)
        {
            if (self.Length > 0)
            {
                self.Insert(0, $"{Environment.NewLine}WHERE ");
            }

            return self.Insert(0, sql);
        }
    }
}