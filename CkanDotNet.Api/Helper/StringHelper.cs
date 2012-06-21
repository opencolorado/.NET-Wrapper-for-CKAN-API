using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CkanDotNet.Api.Helper
{
    public static class StringHelper
    {
        /// <summary>
        /// Creates an abbreviated string 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="contentString"></param>
        /// <returns></returns>
        public static string CreateAbbreviatedString(int length, string contentString)
        {
            if (!String.IsNullOrEmpty(contentString))
            {
                // Strip the HTML
                contentString = Regex.Replace(contentString, @"<(.|\n)*?>", "").Trim();

                // Abbreviate the notes
                if (contentString.Length > length)
                {
                    // Get the nearest space
                    int spaceIndex = contentString.IndexOf(" ", length);

                    if (spaceIndex > 0)
                    {
                        contentString = contentString.Substring(0, spaceIndex) + "...";
                    }
                    else
                    {
                        contentString = contentString.Substring(0, length) + "...";
                    }
                }
            }
            return contentString;
        }
    }
}
