using System;

namespace Opt.Options.Attributes
{
    /// <summary>
    /// Base Class for an OptionAttribute
    /// Has a Name property to use in reflecting out the OptionName
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class OptionAttribute : Attribute
    {
        /// <summary>
        /// The Key to be used when finding the value
        /// </summary>
        public string OptionName { get; private set; }

        /// <summary>
        /// Default constructor for an OptionAttribute
        /// </summary>
        /// <param name="name">Sets the OptionName</param>
        public OptionAttribute(string name)
            : base()
        {
            this.OptionName = name;
        }
    }
}
