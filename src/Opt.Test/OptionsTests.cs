using log4net.Appender;
using log4net.Config;
using Opt.Tests.Stubs;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Opt.Tests
{    
    public class OptionsTests
    {
        [Fact]
        public void UnknownOptions()
        {
            var memoryAppender = new MemoryAppender();
            BasicConfigurator.Configure(memoryAppender);

            Dictionary<string, string> testDictionary = new Dictionary<string, string>();

            Guid g = Guid.NewGuid();
            testDictionary.Add(g.ToString(), g.ToString());
            testDictionary.Add("MyString", "&");

            SimpleOptions so = new SimpleOptions(testDictionary);

            Assert.True(memoryAppender.GetEvents()[5].RenderedMessage.Equals(g.ToString()));
        }

        [Fact]
        public void KnownOptions()
        {
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

            SimpleOptions so = new SimpleOptions(testDictionary);

            Assert.True(so.SimpleStringFeature.Equals(MyStringExpectedValue));
            Assert.True(so.SimpleEnumFeature == EnumFeatureExpectedValue);
            Assert.True(so.FeatureOneIsOn == Feature1ExpectedValue);
            Assert.True(so.FeatureTwoIsOn == Feature2ExpectedValue);
            Assert.True(so.SimpleIntegerProperty == SimpleIntegerExpectedValue);
        }

        [Fact]
        public void NullOptions()
        {
            Assert.Throws(typeof(ArgumentNullException), () =>
             {
                 SimpleOptions so = new SimpleOptions(null);
             });
        }
                
        [Fact]
        public void NullBoth()
        {
            Assert.Throws(typeof(ArgumentNullException), () =>
            {
                SimpleOptions so = new SimpleOptions(null);
            });
        }


        [Fact]
        public void DictionaryTest()
        {
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

            SimpleOptions so = new SimpleOptions(testDictionary);
            
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
