using Opt.Core;
using Opt.Options;
using Opt.Options.Attributes;
using System.Collections.Generic;

namespace Opt.Tests.Stubs
{
    class SimpleOptions : OptionSet
    {        
        #region Properties

        #region Bool Properties
        [BoolOption("Feat1")]
        public bool FeatureOneIsOn { get; private set; }
        [BoolOption("Feat2", true)]
        public bool FeatureTwoIsOn { get; private set; }
        #endregion

        #region Enum Properties
        [EnumOption("EnumFeat", typeof(SimpleEnum), (int)SimpleEnum.ItemOne)]
        public SimpleEnum SimpleEnumFeature { get; private set; }
        #endregion

        #region String Properties
        [StringOption("MyString", "|")]
        public string SimpleStringFeature { get; private set; }
        #endregion

        #endregion

        public SimpleOptions(ILog logger, Dictionary<string, string> keyValue)
            : base(logger, keyValue)
        {

        }
    }
}
