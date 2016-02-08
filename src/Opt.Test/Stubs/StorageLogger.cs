using Orth.Core.Logs;
using System.Collections.Generic;

namespace Opt.Tests.Stubs
{
    /// <summary>
    /// A logger that does not actually log, but stores the logged values in the CachedLogs property
    /// </summary>
    class StorageLogger : ILog
    {
        /// <summary>
        /// Holds the logged strings
        /// </summary>
        public List<string> CachedLogs { get; private set; }

        /// <summary>
        /// Creates a StorageLogger
        /// </summary>
        public StorageLogger()
        {
            CachedLogs = new List<string>();
        }

        /// <summary>
        /// Adds the <paramref name="log"/> value to the <see cref="CachedLogs"/> list
        /// </summary>
        /// <param name="log">The string to log</param>
        public void Log(string log)
        {
            CachedLogs.Add(log);
        }

    }
}
