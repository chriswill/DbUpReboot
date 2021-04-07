using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DbUp.Reboot.Engine.Transactions;
using DbUp.Reboot.SQLite.Helpers;
using Microsoft.Data.Sqlite;

namespace DbUp.Reboot.SQLite
{
    /// <summary>
    /// Connection manager for Sql Lite
    /// </summary>
    public class SQLiteConnectionManager : DatabaseConnectionManager
    {
        /// <summary>
        /// Creates new SQLite Connection Manager
        /// </summary>
        public SQLiteConnectionManager(string connectionString) : base(l => new SqliteConnection(connectionString))
        {
        }

        /// <summary>
        /// Creates new SQLite Connection Manager
        /// </summary>
        public SQLiteConnectionManager(SharedConnection sharedConnection) : base(l => sharedConnection)
        {
        }

        /// <summary>
        /// Sqlite statements separator is ; (see http://www.sqlite.org/lang.html)
        /// </summary>
        public override IEnumerable<string> SplitScriptIntoCommands(string scriptContents)
        {
            var scriptStatements =
                Regex.Split(scriptContents, "^\\s*;\\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline)
                    .Select(x => x.Trim())
                    .Where(x => x.Length > 0)
                    .ToArray();

            return scriptStatements;
        }
    }
}