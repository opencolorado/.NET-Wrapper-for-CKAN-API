﻿using System;
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
                	"M/d/yyyy h:mm:ss tt", // default format for invariant culture
      			"yyyy-MM-dd HH:mm:ssZ", 
                "yyyy-MM-dd HH:mm:ss.ffffff",
                "yyyy-MM-dd HH:mm:ss.ffffffZ",
				"yyyy-MM-ddTHH:mm:ss",
                "yyyy-MM-ddTHH:mm:ssZ", 
				"yyyy-MM-ddTHH:mm:sszzzzzz",
                "yyyy-MM-ddTHH:mm:ss.ffffff",
                "yyyy-MM-ddTHH:mm:ss.fffffffZ",
                "yyyy-MM-ddTHH:mm:ss.ffffffZ",
                "yyyy-MM-ddTHH:mm:ss.fffffZ",
                "yyyy-MM-ddTHH:mm:ss.ffffZ",
                "yyyy-MM-ddTHH:mm:ss.fffZ",
                "yyyy-MM-ddTHH:mm:ss.ffZ",
                "yyyy-MM-ddTHH:mm:ss.fZ",
                "yyyy-MM-ddTHH:mm:ssZ",
                "yyyy-MM-ddTHH:mm:ss.fffffffzzz",
                "yyyy-MM-ddTHH:mm:ss.ffffffzzz",
                "yyyy-MM-ddTHH:mm:ss.fffffzzz",
                "yyyy-MM-ddTHH:mm:ss.ffffzzz",
                "yyyy-MM-ddTHH:mm:ss.fffzzz",
                "yyyy-MM-ddTHH:mm:ss.ffzzz",
                "yyyy-MM-ddTHH:mm:ss.fzzz",
                "yyyy-MM-ddTHH:mm:sszzz"

			};

            DateTime date;
            if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                if (date.Kind != DateTimeKind.Local)
                {
                    date = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local);
                }
                return date;
            }

            return DateTime.MinValue;
        }
    }
}
