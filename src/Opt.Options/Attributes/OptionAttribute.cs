using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opt.Options.Attributes
{
    abstract class OptionAttribute : Attribute
    {
        public string Name { get; private set; }

        public OptionAttribute(string name)
        {
            this.Name = name;
        }
    }
}
