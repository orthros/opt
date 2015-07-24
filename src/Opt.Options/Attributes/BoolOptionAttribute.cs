
namespace Opt.Options.Attributes
{
    public class BoolOptionAttribute : OptionAttribute
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
