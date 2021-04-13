using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Serilog;

namespace OnlineShop.Infrastructure
{
    public static class DbUpdater
    {
        public static void CheckAndUpdateDatabaseVersion(string connectionString)
        {
            string strVersion;
            using (var conn = new SqlConnection(connectionString))
            {
                strVersion = GetDbVersionFromDatabase(conn);
            }

            var currentDbVersion = new Version(strVersion);
            Log.Information("Checking database version. Current version is {dbVersion}", currentDbVersion);

            string prefix;
            try
            {
                var assembly = typeof(DbUpdater).Assembly;

                prefix = assembly.GetName().Name + "._Update.";
                var resourceNames = assembly.GetManifestResourceNames();

                //currentDbVersion
                foreach (var resource in resourceNames
                    .Where(x => x.StartsWith(prefix))
                    .Select(x => new {ResourceName = x, DbVersion = GetDbVersionFromResourceName(x)})
                    .Where(x => x.DbVersion > currentDbVersion)
                    .OrderBy(x => x.DbVersion))
                {
                    Log.Warning("Updating database to version {dbVersion}", resource.DbVersion);

                    using var stream = assembly.GetManifestResourceStream(resource.ResourceName);
                    if (stream is null)
                        continue;

                    using var reader = new StreamReader(stream, Encoding.UTF8);
                    var sql = reader.ReadToEnd();

                    using var conn = new SqlConnection(connectionString);
                    var serverConnection = new ServerConnection(conn);
                    var server = new Server(serverConnection);
                    serverConnection.StatementExecuted += ServerConnection_StatementExecuted;
                    serverConnection.InfoMessage += ServerConnection_InfoMessage;
                    serverConnection.ServerMessage += ServerConnection_ServerMessage;

                    sql = sql.Replace("$(DatabaseName)", serverConnection.DatabaseName,
                        StringComparison.OrdinalIgnoreCase);

                    serverConnection.BeginTransaction();
                    try
                    {
                        // Try to obtain app lock
                        var lockResult = (int) server.ConnectionContext.ExecuteScalar(
                            "DECLARE @r int;EXEC @r=sp_getapplock @Resource='$DbUpdate',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=-1;SELECT @r;");
                        if (lockResult < 0)
                            throw new ApplicationException("Cannot obtain database app lock");

                        // Check database version once again
                        strVersion = GetDbVersionFromDatabase(conn);

                        if (resource.DbVersion > new Version(strVersion)
                        ) // If another service didn't update database already
                        {
                            server.ConnectionContext.ExecuteNonQuery(sql);

                            server.ConnectionContext.ExecuteNonQuery(
                                $"EXEC [dbo].[SetSysSetting] @id = 'DbVersion', @value = {Quote(resource.DbVersion.ToString())};");
                            serverConnection.CommitTransaction();
                        }
                        else
                        {
                            Log.Warning(
                                "Skipped updating database to version {dbVersion}. Other service already updated",
                                resource.DbVersion);
                            serverConnection.RollBackTransaction();
                        }
                    }
                    catch
                    {
                        serverConnection.RollBackTransaction();
                        throw;
                    }
                }
            }
            catch (ExecutionFailureException sqlEx)
            {
                if (sqlEx.InnerException != null)
                    throw sqlEx.InnerException;
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Version GetDbVersionFromResourceName(string resourceName)
            {
                var fileName = resourceName[prefix.Length..];
                return new Version(Path.GetFileNameWithoutExtension(fileName));
            }

            static string GetDbVersionFromDatabase(SqlConnection conn)
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    using var command = new SqlCommand("SELECT [Value] FROM dbo.SysSettings WHERE [Id]=@id", conn);
                    command.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 50).Value = "DbVersion";

                    var o = command.ExecuteScalar();
                    return o is string v ? v : "1.0.0";
                }
                catch (SqlException ex) when (ex.Number == 208 && ex.Class == 16 && ex.State == 1) // "Invalid object name 'dbo.SysSettings'
                {
                    // SysSettings not found, so assume there's no structure yet
                    return "0.0.0";
                }
            }

            // Returns a quoted sql string
            static string Quote(string value) => "'" + value.Replace("'", "''") + "'";
        }

        private static void ServerConnection_ServerMessage(object sender, ServerMessageEventArgs e)
        {
            Log.Verbose("Sql server message: {sql}", e);
        }

        private static void ServerConnection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            Log.Verbose("Sql info message: {sql}", e.Message);
        }

        private static void ServerConnection_StatementExecuted(object sender, StatementEventArgs e)
        {
            Log.Verbose("Sql executed: {sql}", e.SqlStatement);
        }
    }
}