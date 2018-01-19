using System.Text.RegularExpressions;

namespace DOL.WHD.Section14c.Common.Extensions
{
    /// <summary>
    /// Cistom string extension
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Trim string and convert to lower case
        /// </summary>
        /// <param name="value">String to convert</param>
        /// <returns>Convert value</returns>
        public static string TrimAndToLowerCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            return ConvertWhitespacesToSingleSpaces(value).Trim().ToLower();
        }

        /// <summary>
        /// Trim string and 
        /// Convert string whitespaces to single spaces
        /// </summary>
        /// <param name="value">String to convert</param>
        /// <returns>Convert value</returns>
        public static string TrimAndConvertWhitespacesToSingleSpaces(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            return ConvertWhitespacesToSingleSpaces(value).Trim();
        }

        /// <summary>
        /// Convert string whitespaces to single spaces
        /// </summary>
        /// <param name="value">String to convert</param>
        /// <returns>Convert value</returns>
        public static string ConvertWhitespacesToSingleSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }
    }
}
