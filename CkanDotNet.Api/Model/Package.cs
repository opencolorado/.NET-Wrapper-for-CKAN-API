using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CkanDotNet.Api.Helper;
using System.Text.RegularExpressions;

namespace CkanDotNet.Api.Model
{
    public class Package
    {
        private string baseTitle;

        /// <summary>
        /// Gets the auto-generated unique identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets the friendly unique identifier
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the title for display
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets the version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets the URL for the web page describing the data (not the data itself)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets the name of the main contact
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets the email address of the main contact
        /// </summary>
        public string AuthorEmail { get; set; }

        /// <summary>
        /// Gets the name of the maintener (if different from the main contact)
        /// </summary>
        public string Maintainer { get; set; }

        /// <summary>
        /// Gets the email address of the maintener (if different from the main contact)
        /// </summary>
        public string MaintainerEmail { get; set; }

        /// <summary>
        /// Gets any notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets the unique identifier for the license
        /// </summary>
        public string LicenseId { get; set; }

        /// <summary>
        /// Gets the state of the package
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets the most recent revision id
        /// </summary>
        public string RevisionId { get; set; }

        /// <summary>
        /// Gets the name of the license
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// Gets the tags associated with the package
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets the groups associated with the package
        /// </summary>
        public List<string> Groups { get; set; }

        /// <summary>
        /// Get the extra fields associated with the package
        /// </summary>
        public Dictionary<string, string> Extras { get; set; }

        /// <summary>
        /// Gets the average rating
        /// </summary>
        public double? RatingsAverage { get; set; }

        /// <summary>
        /// Gets the number of ratings that have been submitted
        /// </summary>
        public int RatingsCount { get; set; }

        /// <summary>
        /// Get the package resources
        /// </summary>
        public List<Resource> Resources { get; set; }

        /// <summary>
        /// Gets the URL to the package in the CKAN repository
        /// </summary>
        public string CkanUrl { get; set; }

        /// <summary>
        /// Gets the package relationships
        /// </summary>
        public List<Package> Relationships { get; set; }

        /// <summary>
        /// Gets the metadata modified date in ISO string format
        /// </summary>
        public string MetadataModified { get; set; }

        /// <summary>
        /// Gets the metadata created date in ISO string format
        /// </summary>
        public string MetadataCreated { get; set; }

        /// <summary>
        /// Gets the notes rendered date in ISO string format
        /// </summary>
        public string NotesRendered { get; set; }

        /// <summary>
        /// Gets the metadata modified date
        /// </summary>
        public DateTime MetadataModifiedAsDate
        {
            get
            {
                return DateHelper.Parse(MetadataModified);
            }
        }

        /// <summary>
        /// Gets the metadata created date
        /// </summary>
        public DateTime MetadataCreatedAsDate
        {
            get
            {
                return DateHelper.Parse(MetadataCreated);
            }
        }

        /// <summary>
        /// Get the abbreviated notes truncated to a specific size (nearest word).  All markup is 
        /// removed.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GetAbbreviatedNotes(int length)
        {
            // Get the rendered notes
            string summary = NotesRendered;

            // Strip the HTML
            summary = Regex.Replace(summary, @"<(.|\n)*?>", "").Trim();

            // Abbreviate the notes
            if (summary.Length > length)
            {
                // Get the nearest space
                int spaceIndex = summary.IndexOf(" ", length);

                if (spaceIndex > 0) {
                    summary = summary.Substring(0, spaceIndex) + "...";
                }
                else 
                {
                    summary = summary.Substring(0, length) + "...";
                }
            }
            return summary;
        }
    }

}
