using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opt.Tests.Stubs;
using System;
using System.Collections.Generic;

namespace Opt.Tests
{
    [TestClass]
    public class OptionsTests
    {
        [TestMethod]
        public void UnknownOptions()
        {
            StorageLogger logger = new StorageLogger();
            Dictionary<string, string> testDictionary = new Dictionary<string, string>();

            Guid g = Guid.NewGuid();
            testDictionary.Add(g.ToString(), g.ToString());
            testDictionary.Add("MyString", "&");

            SimpleOptions so = new SimpleOptions(logger, testDictionary);

            Assert.IsTrue(logger.CachedLogs.Contains(g.ToString()));
        }

        [TestMethod]
        public void KnownOptions()
        {
            StorageLogger logger = new StorageLogger();
            Dictionary<string, string> testDictionary = new Dictionary<string, string>();

            string MyStringExpectedValue = "&";
            bool Feature1ExpectedValue = true;
            bool Feature2ExpectedValue = true;
            SimpleEnum EnumFeatureExpectedValue = SimpleEnum.ItemThree;
            int SimpleIntegerExpectedValue = 999;
            
            testDictionary.Add("MyString", MyStringExpectedValue);
            testDictionary.Add("EnumFeat",EnumFeatureExpectedValue.ToString());
            testDictionary.Add("Feat1",Feature1ExpectedValue.ToString());
            testDictionary.Add("Feat2",Feature2ExpectedValue.ToString());
            testDictionary.Add("MyInt", SimpleIntegerExpectedValue.ToString());

            SimpleOptions so = new SimpleOptions(logger, testDictionary);

            Assert.IsTrue(so.SimpleStringFeature.Equals(MyStringExpectedValue));
            Assert.IsTrue(so.SimpleEnumFeature == EnumFeatureExpectedValue);
            Assert.IsTrue(so.FeatureOneIsOn == Feature1ExpectedValue);
            Assert.IsTrue(so.FeatureTwoIsOn == Feature2ExpectedValue);
            Assert.IsTrue(so.SimpleIntegerProperty == SimpleIntegerExpectedValue);
        }
    }
}
