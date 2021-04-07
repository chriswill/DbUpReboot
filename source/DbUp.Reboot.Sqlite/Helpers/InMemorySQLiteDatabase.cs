using System;
using DbUp.Reboot.Engine.Transactions;
using DbUp.Reboot.Helpers;
using Microsoft.Data.Sqlite;

namespace DbUp.Reboot.SQLite.Helpers
{
    /// <summary>
    /// Used to create in-memory SQLite database that is deleted at the end of a test.
    /// </summary>
    public class InMemorySQLiteDatabase : IDisposable
    {
        readonly SQLiteConnectionManager connectionManager;
        readonly SqliteConnection sharedConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemorySQLiteDatabase"/> class.
        /// </summary>
        public InMemorySQLiteDatabase()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = ":memory:"
            };
            ConnectionString = connectionStringBuilder.ToString();

            connectionManager = new SQLiteConnectionManager(connectionStringBuilder.ConnectionString);
            sharedConnection = new SqliteConnection(connectionStringBuilder.ConnectionString);
            sharedConnection.Open();
            SqlRunner = new AdHocSqlRunner(() => sharedConnection.CreateCommand(), new SQLiteObjectParser(), null, () => true);
        }

        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets the connection factory of in-memory database.
        /// </summary>
        public IConnectionManager GetConnectionManager() => connectionManager;

        /// <summary>
        /// An adhoc sql runner against the in-memory database
        /// </summary>
        public AdHocSqlRunner SqlRunner { get; }

        /// <summary>
        /// Remove the database from memory.
        /// </summary>
        public void Dispose() => sharedConnection.Dispose();
    }
}
