using System.Collections.Generic;
using DbUp.Reboot.Engine.Transactions;

namespace DbUp.Reboot.Engine
{
    /// <summary>
    /// Provides scripts to be executed.
    /// </summary>
    public interface IScriptProvider
    {
        /// <summary>
        /// Gets all scripts that should be executed.
        /// </summary>
        IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager);
    }
}
