﻿using System.Text.RegularExpressions;
using DbUp.Reboot.Engine;

namespace DbUp.Reboot.SQLite
{
    /// <summary>
    /// This preprocessor makes adjustments to your sql to make it compatible with Sqlite
    /// </summary>
    public class SQLitePreprocessor : IScriptPreprocessor
    {
        /// <summary>
        /// Performs some preprocessing step on a SQLite script
        /// </summary>
        public string Process(string contents) => Regex.Replace(contents, @"n?varchar\s?\(max\)", "text", RegexOptions.IgnoreCase);
    }
}
