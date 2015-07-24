using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opt.Options.Attributes
{
    class BoolOptionAttribute : OptionAttribute
    {
        public bool DefaultValue { get; private set; }

        public BoolOptionAttribute(string name, bool defaultValue)
            : base(name)
        {
            this.DefaultValue = defaultValue;
        }

        public BoolOptionAttribute(string name)
            : this(name, false)
        {

        }
    }
}
