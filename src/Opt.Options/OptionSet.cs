using Opt.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opt.Options
{
    public class OptionSet
    {
        protected ILog Logger{get; private set;}
        
        public OptionSet(ILog log, Dictionary<string,string> keyValues)
        {
            this.Logger = log;
            InitializeProperties(keyValues);
        }

        private void InitializeProperties(Dictionary<string,string> keysAndValues)
        {
            #region Bools
            var boolPropertiesThatAreEngineOptions = from p in this.GetType().GetProperties()
                                                     let attr = p.GetCustomAttributes(typeof(BoolOptionAttribute), true)
                                                     where attr.Length != 0
                                                     select new { Property = p, Attribute = attr.First() as BoolOptionAttribute };

            foreach (var val in boolPropertiesThatAreEngineOptions)
            {
                if (keysAndValues.ContainsKey(val.Attribute.Name))
                {
                    var stringVal = keysAndValues[val.Property.Name];
                    var boolVal = Utils.parseYesNoTrueFalse(stringVal, val.Attribute.DefaultValue);
                    val.Property.SetValue(this, boolVal, null);
                }
                else
                {
                    //Warn
                    Logger.Log(string.Format("Could not find Option {0}. Setting it to the default value \"{1}\"",
                        val.Property.Name, val.Attribute.DefaultValue));
                    //Set
                    val.Property.SetValue(this, val.Attribute.DefaultValue, null);
                }
            }
            #endregion

            #region Enums
            var enumPropertiesThatAreEngineOptions = from p in this.GetType().GetProperties()
                                                     let attr = p.GetCustomAttributes(typeof(EnumOptionAttribute), true)
                                                     where attr.Length != 0
                                                     select new { Property = p, Attribute = attr.First() as EnumOptionAttribute };

            foreach (var val in enumPropertiesThatAreEngineOptions)
            {
                if (keysAndValues.ContainsKey(val.Attribute.Name))
                {
                    var stringValue = keysAndValues[val.Property.Name];
                    var enumVal = Enum.Parse(val.Attribute.EnumType, stringValue);
                    val.Property.SetValue(this, enumVal, null);
                }
                else
                {
                    //Warn
                    var enumValue = Enum.Parse(val.Attribute.EnumType, val.Attribute.DefaultValue.ToString());
                    Logger.Log(string.Format("Could not find Option {0}. Setting it to the default value \"{1}\"",
                        val.Property.Name, enumValue));
                    //Set
                    val.Property.SetValue(this, enumValue, null);
                }
            }

            #endregion

            #region Strings
            var stringPropertiesThatAreEngineOptions = from p in this.GetType().GetProperties()
                                                       let attr = p.GetCustomAttributes(typeof(StringOptionAttribute), true)
                                                       where attr.Length != 0
                                                       select new { Property = p, Attribute = attr.First() as StringOptionAttribute };

            foreach (var val in stringPropertiesThatAreEngineOptions)
            {
                if (keysAndValues.ContainsKey(val.Attribute.Name))
                {
                    var stringVal = keysAndValues[val.Property.Name];
                    val.Property.SetValue(this, stringVal, null);
                }
                else
                {
                    //Warn
                    Logger.Log(string.Format("Could not find Option \"{0}\". Setting it to the default value \"{1}\"",
                        val.Property.Name, val.Attribute.DefaultStringValue));
                    //Set
                    val.Property.SetValue(this, val.Attribute.DefaultStringValue, null);
                }
            }
            #endregion            
        }

        abstract class OptionAttribute : Attribute
        {
            public string Name { get; private set; }

            public OptionAttribute(string name)
            {
                this.Name = name;
            }
        }

        class EnumOptionAttribute : OptionAttribute
        {
            public int DefaultValue { get; private set; }
            public Type EnumType { get; private set; }

            public EnumOptionAttribute(string name, Type enumType, int defaultvalue)
                : base(name)
            {
                this.DefaultValue = defaultvalue;
                this.EnumType = enumType;
            }
        }

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

        class StringOptionAttribute : OptionAttribute
        {
            public string DefaultStringValue { get; private set; }

            public StringOptionAttribute(string name, string defaultValue)
                : base(name)
            {
                this.DefaultStringValue = defaultValue;
            }
        }

    }
}
