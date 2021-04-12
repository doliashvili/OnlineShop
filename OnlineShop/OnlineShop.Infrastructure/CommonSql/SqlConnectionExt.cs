using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace OnlineShop.Infrastructure.CommonSql
{
    public static class SqlConnectionExt
    {
        public static Task EnsureIsOpenAsync(this SqlConnection self, CancellationToken cancellationToken = default) =>
            self.State != ConnectionState.Open ? self.OpenAsync(cancellationToken) : Task.CompletedTask;

        public static void EnsureIsOpen(this SqlConnection self)
        {
            if (self.State != ConnectionState.Open)
                self.Open();
        }
    }
}