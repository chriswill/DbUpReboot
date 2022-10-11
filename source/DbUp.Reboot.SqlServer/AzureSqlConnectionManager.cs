using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DbUp.Reboot.Engine.Transactions;
using DbUp.Reboot.Support;
using Microsoft.Identity.Client;
using Azure.Identity;
using Azure.Core;

namespace DbUp.Reboot.SqlServer
{
    /// <summary>Manages an Azure Sql Server database connection.</summary>
    public class AzureSqlConnectionManager : DatabaseConnectionManager
    {
        public AzureSqlConnectionManager(string connectionString)
            : this(connectionString, "https://database.windows.net/", null)
        { }

        public AzureSqlConnectionManager(string connectionString, string resource)
            : this(connectionString, resource, null)
        { }

        public AzureSqlConnectionManager(string connectionString, string resource, string tenantId, string azureAdInstance = "https://login.microsoftonline.com/")
            : base(new DelegateConnectionFactory((log, dbManager) =>
            {
                if (dbManager.IsScriptOutputLogged)
                    log.WriteInformation($"AzureSqlConnectionManager connection string: {connectionString} resource: {resource}");

                // Check if the user assigned managed identity app id is set in the User Id of the connection string.
                // If so, no need to get a token as the SQL client automatically works with Azure Identity behind the scenes to get a token. 
                var r = new System.Text.RegularExpressions.Regex("(?<UserID>User ID=[a-z|A-Z|0-9|\\-]*)");
                var m = r.Match(connectionString);
                var hasUserIDInConnectionString = m.Success;

                var conn = new SqlConnection(connectionString);

                if (!hasUserIDInConnectionString)
                {
                    if (dbManager.IsScriptOutputLogged)
                        log.WriteInformation($"AzureSqlConnectionManager request an access token since the User ID of the managed identity was not passed in the User ID connection string parameter");

                    var tokenRequestContext = new TokenRequestContext(scopes: new string[] { resource + "/.default" }) { };
                    var accessToken = new DefaultAzureCredential().GetTokenAsync(tokenRequestContext)
                                                                  .ConfigureAwait(false)
                                                                  .GetAwaiter()
                                                                  .GetResult()
                                                                  .Token;
                    if (dbManager.IsScriptOutputLogged)
                        log.WriteInformation($"AzureSqlConnectionManager access token: {accessToken}");

                    conn.AccessToken = accessToken;
                } else
                {
                    if (dbManager.IsScriptOutputLogged)
                        log.WriteInformation($"AzureSqlConnectionManager User ID of the managed identity provided in the connection string: {m.Groups["UserID"].Value}");
                }

                if (dbManager.IsScriptOutputLogged)
                    conn.InfoMessage += (sender, e) => log.WriteInformation($"{{0}}", e.Message);

                return conn;
            }))
        { }

        public override IEnumerable<string> SplitScriptIntoCommands(string scriptContents)
        {
            var commandSplitter = new SqlCommandSplitter();
            var scriptStatements = commandSplitter.SplitScriptIntoCommands(scriptContents);
            return scriptStatements;
        }
    }
}
