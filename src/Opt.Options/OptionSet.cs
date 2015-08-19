﻿using Opt.Core;
using Opt.Options.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opt.Options
{
    /// <summary>
    /// Base class to handle various options
    /// Uses Reflection and <see cref="OptionAttribute"/> to automatically
    /// set properties in the option set
    /// </summary>
    public abstract class OptionSet
    {
        /// <summary>
        /// Provides logging functionality
        /// </summary>
        protected ILog Logger { get; private set; }
                
        
        /// <summary>
        /// Constructor for the OptionSet
        /// Parses the Options out of the dictionary
        /// </summary>
        /// <param name="log">The log for the OptionSet to use</param>
        /// <param name="keyValues">The set of Options represented as strings</param>
        public OptionSet(ILog log, Dictionary<string,string> keyValues)
        {
            this.Logger = log;
            InitializeProperties(keyValues);
        }

        /// <summary>
        /// Initializes the Properties on the OptionSet object using reflection and <see cref="OptionAttribute"/>
        /// Will log occurances of unknown or unset options.
        /// Uses the <see cref="OptionAttribute"/>'s Name property as a key in <paramref name="keyValues"/> Dictionary
        /// </summary>
        /// <param name="keyValues">The Keys and Values of the Options</param>
        private void InitializeProperties(Dictionary<string,string> keyValues)
        {
            #region Bools
            var boolPropertiesThatAreOptions = from p in this.GetType().GetProperties()
                                                     let attr = p.GetCustomAttributes(typeof(BoolOptionAttribute), true)
                                                     where attr.Length != 0
                                                     select new { Property = p, Attribute = attr.First() as BoolOptionAttribute };

            foreach (var val in boolPropertiesThatAreOptions)
            {
                if (keyValues.ContainsKey(val.Attribute.OptionName))
                {
                    var stringVal = keyValues[val.Attribute.OptionName];
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
                if (keyValues.ContainsKey(val.Attribute.OptionName))
                {
                    var stringValue = keyValues[val.Attribute.OptionName];
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
                if (keyValues.ContainsKey(val.Attribute.OptionName))
                {
                    var stringVal = keyValues[val.Attribute.OptionName];
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

            #region Integerss
            var integerPropertiesThatAreOptions = from p in this.GetType().GetProperties()
                                                    let attr = p.GetCustomAttributes(typeof(IntegerOptionAttribute), true)
                                                    where attr.Length != 0
                                                    select new { Property = p, Attribute = attr.First() as IntegerOptionAttribute };

            foreach (var val in integerPropertiesThatAreOptions)
            {
                if (keyValues.ContainsKey(val.Attribute.OptionName))
                {
                    var stringVal = keyValues[val.Attribute.OptionName];
                    var integerValue = val.Attribute.DefaultIntegerValue;
                    if(!int.TryParse(stringVal, out integerValue))
                    {
                        integerValue = val.Attribute.DefaultIntegerValue;
                    }
                    val.Property.SetValue(this, integerValue, null);
                }
                else
                {
                    //Warn
                    Logger.Log(string.Format("Could not find Option \"{0}\". Setting it to the default value \"{1}\"",
                        val.Property.Name, val.Attribute.DefaultIntegerValue));
                    //Set
                    val.Property.SetValue(this, val.Attribute.DefaultIntegerValue, null);
                }
            }
            #endregion

            #region Check for unknowns
            var unknownKVP = keyValues
                .Select(x => x.Key)
                .Except(stringPropertiesThatAreOptions.Select(x => x.Attribute.OptionName))
                .Except(boolPropertiesThatAreOptions.Select(x => x.Attribute.OptionName))
                .Except(enumPropertiesThatAreOptions.Select(x => x.Attribute.OptionName))
                .Except(integerPropertiesThatAreOptions.Select(x => x.Attribute.OptionName));

            if(unknownKVP.Any())
            {
                Logger.Log("The following Options were not found:\n\t");
                Logger.Log(string.Join("\n\t", unknownKVP));
            }

            #endregion
        }
    }
}
