using System.Collections.Generic;
using System.Data;
using DbUp.Reboot.Engine;
using DbUp.Reboot.Engine.Output;
using DbUp.Reboot.Engine.Transactions;

namespace DbUp.Reboot.Tests.TestInfrastructure
{
    public class TestConnectionManager : DatabaseConnectionManager
    {
        public TestConnectionManager(IDbConnection connection, bool startUpgrade = false) : base(l => connection)
        {
            if (startUpgrade)
                OperationStarting(new ConsoleUpgradeLog(), new List<SqlScript>());
        }

        public override IEnumerable<string> SplitScriptIntoCommands(string scriptContents)
        {
            return new[] { scriptContents };
        }
    }
}
