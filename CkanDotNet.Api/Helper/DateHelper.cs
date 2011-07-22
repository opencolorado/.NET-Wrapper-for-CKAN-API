using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace CkanDotNet.Api.Helper
{
    public static class DateHelper
    {
        /// <summary>
        /// Parse a date/time string.
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime Parse(string dateString)
        {
            // Remove any line breaks
            dateString = dateString.Replace("\n", "");
            dateString = dateString.Replace("\r", "");

            // Remove any surrounding quotes
            if (dateString.StartsWith("\"") && dateString.EndsWith("\""))
            {
                dateString = dateString.Substring(1, dateString.Length - 2);
            }

            var formats = new[] {
				"u", 
				"s", 
				"yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", 
				"yyyy-MM-ddTHH:mm:ssZ", 
				"yyyy-MM-dd HH:mm:ssZ", 
				"yyyy-MM-ddTHH:mm:ss", 
				"yyyy-MM-ddTHH:mm:sszzzzzz",
                "yyyy-MM-ddTHH:mm:ss.ffffff",
                "yyyy-MM-dd HH:mm:ss.ffffff",
				"M/d/yyyy h:mm:ss tt" // default format for invariant culture
			};

            DateTime date;
            if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }

            return DateTime.MinValue;
        }
    }
}
