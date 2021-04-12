namespace OnlineShop.Infrastructure.CommonSql
{
    /// <summary>
    /// Wraps connection string value.
    /// Should be registered in DI Container with corresponding value
    /// </summary>
    public sealed class DatabaseConnectionString
    {
        /// <summary>
        /// Value of database connection string
        /// </summary>
        public string Value { get; }

        public DatabaseConnectionString(string value)
        {
            Value = value;
        }
    }
}