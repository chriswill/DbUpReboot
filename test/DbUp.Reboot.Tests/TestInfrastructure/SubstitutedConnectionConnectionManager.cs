using System.Collections.Generic;
using System.Data;
using DbUp.Reboot.Engine.Transactions;

namespace DbUp.Reboot.Tests
{
    class SubstitutedConnectionConnectionManager : DatabaseConnectionManager
    {
        public SubstitutedConnectionConnectionManager(IDbConnection conn) : base(l => conn)
        {
        }

        public override IEnumerable<string> SplitScriptIntoCommands(string scriptContents)
        {
            yield return scriptContents;
        }
    }
}