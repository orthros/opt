using Opt.Core;
using Opt.Options.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opt.Options
{
    public abstract class OptionSet
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
            var boolPropertiesThatAreOptions = from p in this.GetType().GetProperties()
                                                     let attr = p.GetCustomAttributes(typeof(BoolOptionAttribute), true)
                                                     where attr.Length != 0
                                                     select new { Property = p, Attribute = attr.First() as BoolOptionAttribute };

            foreach (var val in boolPropertiesThatAreOptions)
            {
                if (keysAndValues.ContainsKey(val.Attribute.Name))
                {
                    var stringVal = keysAndValues[val.Attribute.Name];
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
            var enumPropertiesThatAreOptions = from p in this.GetType().GetProperties()
                                                     let attr = p.GetCustomAttributes(typeof(EnumOptionAttribute), true)
                                                     where attr.Length != 0
                                                     select new { Property = p, Attribute = attr.First() as EnumOptionAttribute };

            foreach (var val in enumPropertiesThatAreOptions)
            {
                if (keysAndValues.ContainsKey(val.Attribute.Name))
                {
                    var stringValue = keysAndValues[val.Attribute.Name];
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
            var stringPropertiesThatAreOptions = from p in this.GetType().GetProperties()
                                                       let attr = p.GetCustomAttributes(typeof(StringOptionAttribute), true)
                                                       where attr.Length != 0
                                                       select new { Property = p, Attribute = attr.First() as StringOptionAttribute };

            foreach (var val in stringPropertiesThatAreOptions)
            {
                if (keysAndValues.ContainsKey(val.Attribute.Name))
                {
                    var stringVal = keysAndValues[val.Attribute.Name];
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
 
            #region Check for unknowns
            var unknownKVP = keysAndValues
                .Select(x => x.Key)                
                .Except(stringPropertiesThatAreOptions.Select(x => x.Attribute.Name))
                .Except(boolPropertiesThatAreOptions.Select(x => x.Attribute.Name))
                .Except(enumPropertiesThatAreOptions.Select(x => x.Attribute.Name));

            if(unknownKVP.Any())
            {
                Logger.Log("The following Options were not found:\n\t");
                Logger.Log(string.Join("\n\t", unknownKVP));
            }

            #endregion
        }
    }
}
