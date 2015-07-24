
namespace Opt.Options.Attributes
{
    /// <summary>
    /// Attribute to decorate a Property to use a string Option in an OptionSet
    /// </summary>
    public class StringOptionAttribute : OptionAttribute
    {
        /// <summary>
        /// The value to use if the Option is unset
        /// </summary>
        public string DefaultStringValue { get; private set; }

        /// <summary>
        /// Default Constructor for a StringOptionAttribute, calls base
        /// </summary>
        /// <param name="optionName">Sets the OptionName</param>
        /// <param name="defaultValue">The value to use if the Option is unset</param>
        public StringOptionAttribute(string optionName, string defaultValue)
            : base(optionName)
        {
            this.DefaultStringValue = defaultValue;
        }
    }
}
