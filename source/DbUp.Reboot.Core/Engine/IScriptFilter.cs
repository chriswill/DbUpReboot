using System.Collections.Generic;
using DbUp.Reboot.Support;

namespace DbUp.Reboot.Engine
{
    public interface IScriptFilter
    {
        IEnumerable<SqlScript> Filter(IEnumerable<SqlScript> sorted, HashSet<string> executedScriptNames, ScriptNameComparer comparer);
    }
}
