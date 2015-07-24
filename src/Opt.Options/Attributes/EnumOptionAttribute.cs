using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opt.Options.Attributes
{
    class EnumOptionAttribute : OptionAttribute
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
