using System.Reflection;
using Opt.Options.Attributes;

namespace Opt.Options
{
    /// <summary>
    /// Class for use in getting properties and attributes that inherit from OptionAttribute
    /// </summary>
    /// <typeparam name="T">The type to use. Must inherit from OptionAttribute</typeparam>
    internal class PropertyOption<T> where T : OptionAttribute
    {
        public PropertyInfo Property { get; private set; }
        public T Attribute { get; private set; }

        public PropertyOption(PropertyInfo propinfo, T attributeInfo)
            : base()
        {
            this.Property = propinfo;
            this.Attribute = attributeInfo;
        }
    }
}
