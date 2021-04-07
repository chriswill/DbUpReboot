#if !NETCORE
using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Xunit;

namespace DbUp.Reboot.Tests.Support.SQLite
{
    public class SQLiteSupportTests
    {
        static readonly string dbFilePath = Path.Combine(Environment.CurrentDirectory, "test.db");

        [Fact]
        public void CanUseSQLite()
        {
            var connectionString = $"Data Source={dbFilePath}; Version=5;";

            if (!File.Exists(dbFilePath))
            {
                File.Create(dbFilePath);
            }

            var upgrader = DeployChanges.To
                .SQLiteDatabase(connectionString)
                .WithScript("Script0001", "CREATE TABLE IF NOT EXISTS Foo (Id int)")
                .Build();
        }
    }
}
#endif