using System;

namespace Opt.Options.Attributes
{
    /// <summary>
    /// Used to Decorate a Property whose value is an Enum used as a Option in an OptionSet
    /// </summary>    
    public class EnumOptionAttribute : OptionAttribute
    {
        /// <summary>
        /// The value to use if the Option is unset
        /// </summary>
        public int DefaultValue { get; private set; }
        
        /// <summary>
        /// The type of Enumeration to use
        /// </summary>
        public Type EnumType { get; private set; }

        /// <summary>
        /// Creates an EnumOptionAttribute. Note, the default value MUST be an int as Enum values
        /// cannot be passed as arguments to an Attribute Constructor. It is recommended you 
        /// cast your default Enum value to an int as opposed to hard-coding an integer value
        /// i.e. (int)SomeEnum.DesiredValue
        /// </summary>
        /// <param name="optionName">Sets the OptionName</param>
        /// <param name="enumType">The type of the enumeration to use</param>
        /// <param name="defaultvalue">The value to use if the Option is unset</param>
        public EnumOptionAttribute(string optionName, Type enumType, int defaultvalue)
            : base(optionName)
        {
            this.DefaultValue = defaultvalue;
            this.EnumType = enumType;
        }
    }
}
