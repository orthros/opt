using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opt.Tests.Stubs;
using System;
using System.Collections.Generic;

namespace Opt.Tests
{
    [TestClass]
    public class UnknownOptionsTests
    {
        [TestMethod]
        public void UnknownOptions()
        {
            StorageLogger logger = new StorageLogger();
            Dictionary<string, string> testDictionary = new Dictionary<string, string>();

            Guid g = Guid.NewGuid();
            testDictionary.Add(g.ToString(), g.ToString());

            SimpleOptions so = new SimpleOptions(logger, testDictionary);

            Assert.IsTrue(logger.CachedLogs.Contains(g.ToString()));
        }
    }
}
