using DbUp.Reboot.Support;

namespace DbUp.Reboot.SQLite
{
    /// <summary>
    /// Parses Sql Objects and performs quoting functions.
    /// </summary>
    public class SQLiteObjectParser : SqlObjectParser
    {
        public SQLiteObjectParser()
            : base("[", "]")
        {
        }
    }
}
