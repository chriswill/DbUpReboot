﻿#if !NETCORE
using System.Data;
using DbUp.Reboot.Engine;
using DbUp.Reboot.Engine.Output;
using DbUp.Reboot.Engine.Transactions;
using DbUp.Reboot.SQLite;
using DbUp.Reboot.Tests.TestInfrastructure;
using Microsoft.Data.Sqlite;
using NSubstitute;
using Shouldly;
using Xunit;

namespace DbUp.Reboot.Tests.Support.SQLite
{
    public class SQLiteTableJournalTests
    {
        [Fact]
        public void dbversion_is_zero_when_journal_table_not_exist()
        {
            // Given
            var dbConnection = Substitute.For<IDbConnection>();
            var command = Substitute.For<IDbCommand>();
            dbConnection.CreateCommand().Returns(command);
            var connectionManager = Substitute.For<IConnectionManager>();
            command.ExecuteScalar().Returns(x => { throw new SqliteException("table not found", 400); });
            var consoleUpgradeLog = new ConsoleUpgradeLog();
            var journal = new SQLiteTableJournal(() => connectionManager, () => consoleUpgradeLog, "SchemaVersions");

            // When
            var scripts = journal.GetExecutedScripts();

            // Expect
            command.DidNotReceive().ExecuteReader();
            scripts.ShouldBeEmpty();
        }

        [Fact]
        public void creates_a_new_journal_table_when_not_exist()
        {
            // Given
            var dbConnection = Substitute.For<IDbConnection>();
            var connectionManager = new TestConnectionManager(dbConnection, true);
            var command = Substitute.For<IDbCommand>();
            var param1 = Substitute.For<IDbDataParameter>();
            var param2 = Substitute.For<IDbDataParameter>();
            dbConnection.CreateCommand().Returns(command);
            command.CreateParameter().Returns(param1, param2);
            command.ExecuteScalar().Returns(x => 0);
            var consoleUpgradeLog = new ConsoleUpgradeLog();
            var journal = new SQLiteTableJournal(() => connectionManager, () => consoleUpgradeLog, "SchemaVersions");

            // When
            journal.StoreExecutedScript(new SqlScript("test", "select 1"), () => command);

            // Expect
            command.Received(2).CreateParameter();
            param1.ParameterName.ShouldBe("scriptName");
            param2.ParameterName.ShouldBe("applied");
            command.Received().ExecuteNonQuery();
        }
    }
}
#endif