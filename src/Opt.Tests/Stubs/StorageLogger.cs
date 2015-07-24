using Opt.Core;
using System.Collections.Generic;

namespace Opt.Tests.Stubs
{
    class StorageLogger : ILog
    {
        /// <summary>
        /// Holds the logged strings
        /// </summary>
        public List<string> CachedLogs { get; private set; }

        public StorageLogger()
        {
            CachedLogs = new List<string>();
        }

        public void Log(string log)
        {
            CachedLogs.Add(log);
        }

    }
}
