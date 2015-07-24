
namespace Opt.Options.Attributes
{
    /// <summary>
    /// /// Attribute to decorate a Property to use a bool Option in an OptionSet
    /// </summary>
    public class BoolOptionAttribute : OptionAttribute
    {
        /// <summary>
        /// The value to use if the Option is unset
        /// </summary>
        public bool DefaultValue { get; private set; }

        /// <summary>
        /// Def
        /// </summary>
        /// <param name="name">Sets the OptionName</param>
        /// <param name="defaultValue">The value to use when the Option is not found</param>
        public BoolOptionAttribute(string name, bool defaultValue)
            : base(name)
        {
            this.DefaultValue = defaultValue;
        }

        /// <summary>
        /// Default constructor for BoolOptionAttribute. Sets the DefaultValue to "False"
        /// </summary>
        /// <param name="name">Sets the OptionName</param>
        public BoolOptionAttribute(string name)
            : this(name, false)
        {

        }
    }    
}
