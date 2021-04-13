using System;
using Microsoft.Data.SqlClient;

namespace OnlineShop.Infrastructure.CommonSql
{
    public static class SqlParameterExt
    {
        //#pragma warning disable CS8603 // Possible null reference return.
        //        public static T GetValue<T>(this SqlParameter self, T defaultValue = default) =>
        //            self.Value == DBNull.Value
        //                ? defaultValue
        //                : (T)self.Value;

        //#pragma warning restore CS8603 // Possible null reference return.

        public static void SetValue<T>(this SqlParameter self, T? value) where T : struct =>
          self.Value = value switch
          {
              null => DBNull.Value,
              _ => value.Value
          };

        public static void SetValue(this SqlParameter self, string? value) =>
         self.Value = value switch
         {
             null => DBNull.Value,
             _ => value
         };

        public static void SetValue(this SqlParameter self, Date value) => self.Value = (DateTime)value;

        public static void SetValue(this SqlParameter self, Date? value) =>
            self.Value = value switch
            {
                null => DBNull.Value,
                _ => (DateTime)value.Value
            };

        //public static void SetValue(this SqlParameter self, byte[]? value) =>
        //   self.Value = value switch
        //   {
        //       null => DBNull.Value,
        //       _ => value
        //   };

        //public static SqlParameter[] ToArray(this SqlParameter self) => new[] { self };
    }
}