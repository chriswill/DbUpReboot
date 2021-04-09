using DbUp.Reboot.Support;

namespace DbUp.Reboot.MySql
{
    /// <summary>
    /// Parses Sql Objects and performs quoting functions
    /// </summary>
    public class MySqlObjectParser : SqlObjectParser
    {
        public MySqlObjectParser() : base("`", "`")
        {
        }
    }
}
