using Opt.Tests.Stubs;
using Orth.Core.Logs;
using System;
using System.Collections.Generic;
using Xunit;

namespace Opt.Tests
{    
    public class OptionsTests
    {
        [Fact]
        public void UnknownOptions()
        {
            StorageLogger logger = new StorageLogger();
            Dictionary<string, string> testDictionary = new Dictionary<string, string>();

            Guid g = Guid.NewGuid();
            testDictionary.Add(g.ToString(), g.ToString());
            testDictionary.Add("MyString", "&");

            SimpleOptions so = new SimpleOptions(logger, testDictionary);

            Assert.True(logger.CachedLogs.Contains(g.ToString()));
        }

        [Fact]
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

            Assert.True(so.SimpleStringFeature.Equals(MyStringExpectedValue));
            Assert.True(so.SimpleEnumFeature == EnumFeatureExpectedValue);
            Assert.True(so.FeatureOneIsOn == Feature1ExpectedValue);
            Assert.True(so.FeatureTwoIsOn == Feature2ExpectedValue);
            Assert.True(so.SimpleIntegerProperty == SimpleIntegerExpectedValue);
        }

        [Fact]
        public void NullOptions()
        {
            StorageLogger logger = new StorageLogger();
            Assert.Throws(typeof(ArgumentNullException), () =>
             {
                 SimpleOptions so = new SimpleOptions(logger, null);
             });
        }

        [Fact]
        public void NullLog()
        {
            Dictionary<string, string> emptyDict = new Dictionary<string, string>();
            Assert.Throws(typeof(ArgumentNullException), () =>
             {
                 SimpleOptions so = new SimpleOptions(null, emptyDict);
             });
        }

        [Fact]
        public void NullBoth()
        {
            Assert.Throws(typeof(ArgumentNullException), () =>
            {
                SimpleOptions so = new SimpleOptions(null, null);
            });
        }


        [Fact]
        public void DictionaryTest()
        {
            StorageLogger logger = new StorageLogger();

            Dictionary<string, string> testDictionary = new Dictionary<string, string>();

            string MyStringExpectedValue = "&";
            bool Feature1ExpectedValue = true;
            bool Feature2ExpectedValue = true;
            SimpleEnum EnumFeatureExpectedValue = SimpleEnum.ItemThree;
            int SimpleIntegerExpectedValue = 999;

            testDictionary.Add("MyString", MyStringExpectedValue);
            testDictionary.Add("EnumFeat", EnumFeatureExpectedValue.ToString());
            testDictionary.Add("Feat1", Feature1ExpectedValue.ToString());
            testDictionary.Add("Feat2", Feature2ExpectedValue.ToString());
            testDictionary.Add("MyInt", SimpleIntegerExpectedValue.ToString());

            SimpleOptions so = new SimpleOptions(logger, testDictionary);
            
            var dictionaryToTest = so.CreateDictionary();

            foreach(var kvp in testDictionary)
            {
                Assert.True(dictionaryToTest.ContainsKey(kvp.Key),
                    string.Format("The resultant dictionary does not contain the key: {0}", kvp.Key));

                Assert.Equal(kvp.Value, testDictionary[kvp.Key]);
            }

            foreach (var kvp in dictionaryToTest)
            {
                Assert.True(dictionaryToTest.ContainsKey(kvp.Key),
                    string.Format("The resultant dicitonary had an unexpected key: {0}", kvp.Key));
            }
        }
    }
}
