using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opt.Options.Attributes
{
    public class IntegerOptionAttribute : OptionAttribute
    {
        /// <summary>
        /// The value to use if the Option is unset.
        /// </summary>
        public int DefaultIntegerValue { get; private set; }

        public IntegerOptionAttribute(string name, int defaultValue) 
            : base(name)
        {
            this.DefaultIntegerValue = defaultValue;
        }
    }
}
