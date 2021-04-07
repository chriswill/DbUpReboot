using System.Data;
using DbUp.Reboot.Builder;
using DbUp.Reboot.Engine.Transactions;

namespace DbUp.Reboot.Tests.TestInfrastructure
{
    /// <summary>
    /// Configures DbUp to use SqlServer with a fake connection
    /// </summary>
    public static class TestDatabaseExtension
    {
        public static UpgradeEngineBuilder OverrideConnectionFactory(this UpgradeEngineBuilder engineBuilder, IDbConnection connection)
        {
            return engineBuilder.OverrideConnectionFactory(new DelegateConnectionFactory(l => connection));
        }

        public static UpgradeEngineBuilder OverrideConnectionFactory(this UpgradeEngineBuilder engineBuilder, IConnectionFactory connectionFactory)
        {
            engineBuilder.Configure(c => ((DatabaseConnectionManager)c.ConnectionManager).OverrideFactoryForTest(connectionFactory));
            return engineBuilder;
        }

        public static UpgradeEngineBuilder TestDatabase(this SupportedDatabases supportedDatabases, IDbConnection connection)
        {
            var builder = supportedDatabases.SqlDatabase("");
            builder.OverrideConnectionFactory(connection);
            return builder;
        }
    }
}