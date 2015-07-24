using System;

namespace Opt.Options.Attributes
{
    public abstract class OptionAttribute : Attribute
    {
        public string Name { get; private set; }

        public OptionAttribute(string name)
        {
            this.Name = name;
        }
    }
}
