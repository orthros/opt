using System;

namespace Opt.Options.Attributes
{
    public class EnumOptionAttribute : OptionAttribute
    {
        public int DefaultValue { get; private set; }
        public Type EnumType { get; private set; }

        public EnumOptionAttribute(string name, Type enumType, int defaultvalue)
            : base(name)
        {
            this.DefaultValue = defaultvalue;
            this.EnumType = enumType;
        }
    }
}
