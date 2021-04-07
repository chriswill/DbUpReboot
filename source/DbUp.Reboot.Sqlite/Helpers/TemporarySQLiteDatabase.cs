using System;
using System.IO;
using DbUp.Reboot.Helpers;
using Microsoft.Data.Sqlite;


namespace DbUp.Reboot.SQLite.Helpers
{
    /// <summary>
    /// Used to create SQLite databases that are deleted at the end of a test.
    /// </summary>
    public class TemporarySQLiteDatabase : IDisposable
    {
        readonly string dataSourcePath;
        readonly SqliteConnection sqLiteConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporarySQLiteDatabase"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TemporarySQLiteDatabase(string name)
        {
            dataSourcePath = Path.Combine(Directory.GetCurrentDirectory(), name);

            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = name
            };

            sqLiteConnection = new SqliteConnection(connectionStringBuilder.ConnectionString);
            sqLiteConnection.Open();
            SharedConnection = new SharedConnection(sqLiteConnection);
            SqlRunner = new AdHocSqlRunner(() => sqLiteConnection.CreateCommand(), new SQLiteObjectParser(), null, () => true);
        }

        /// <summary>
        /// An adhoc sql runner against the temporary database
        /// </summary>
        public AdHocSqlRunner SqlRunner { get; }

        public SharedConnection SharedConnection { get; }

        /// <summary>
        /// Creates the database.
        /// </summary>
        public void Create()
        {
        }

        /// <summary>
        /// Deletes the database.
        /// </summary>
        public void Dispose()
        {
            var filePath = new FileInfo(dataSourcePath);
            if (!filePath.Exists) return;
            SharedConnection.Dispose();
            sqLiteConnection.Dispose();
            File.Delete(dataSourcePath);
        }
    }
}
