using System;
using System.Collections.Generic;
using DbUp.Reboot.Engine;
using DbUp.Reboot.Engine.Transactions;

namespace DbUp.Reboot.ScriptProviders
{
    /// <summary>
    /// Allows you to easily programmatically supply scripts from code.
    /// </summary>
    public sealed class StaticScriptProvider : IScriptProvider
    {
        readonly IEnumerable<SqlScript> scripts;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticScriptProvider"/> class.
        /// </summary>
        /// <param name="scripts">The scripts.</param>
        public StaticScriptProvider(IEnumerable<SqlScript> scripts)
        {
            this.scripts = scripts ?? throw new ArgumentNullException(nameof(scripts));
        }

        /// <summary>
        /// Gets all scripts that should be executed.
        /// </summary>
        public IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager) => scripts;
    }
}
