
namespace Opt.Core
{
    /// <summary>
    /// Class for generic utilities
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Parses a string into true or false. If the string cannot be parsed, it returns the default value
        /// </summary>
        /// <param name="value">The string to parse</param>
        /// <param name="defaultValue">The default value to use if the string was not able to be parsed</param>
        /// <returns></returns>
        public static bool parseYesNoTrueFalse(string value, bool defaultValue = false)
        {
            bool retVal = defaultValue;

            if (!string.IsNullOrWhiteSpace(value))
            {
                switch (value.ToLowerInvariant())
                {
                    case "yes":
                    case "y":
                    case "true":
                    case "t":
                    case "on":
                    case "1":
                        retVal = true;
                        break;
                    case "no":
                    case "n":
                    case "false":
                    case "f":
                    case "off":
                    case "0":
                        retVal = false;
                        break;
                }
            }
            return retVal;
        }
    }
}
