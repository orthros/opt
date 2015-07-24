﻿
namespace Opt.Options.Attributes
{
    public class StringOptionAttribute : OptionAttribute
    {
        public string DefaultStringValue { get; private set; }

        public StringOptionAttribute(string name, string defaultValue)
            : base(name)
        {
            this.DefaultStringValue = defaultValue;
        }
    }
}
