﻿using System;
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

        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public string AuthorEmail { get; set; }
        public string Maintainer { get; set; }
        public string MaintainerEmail { get; set; }
        public string Notes { get; set; }
        public string LicenseId { get; set; }
        public string State { get; set; }
        public string RevisionId { get; set; }
        public string License { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Groups { get; set; }
        public Dictionary<string, string> Extras { get; set; }
        public double? RatingsAverage { get; set; }
        public int RatingsCount { get; set; }
        public List<Resource> Resources { get; set; }
        public string CkanUrl { get; set; }
        public List<Package> Relationships { get; set; }
        public string MetadataModified { get; set; }
        public string MetadataCreated { get; set; }
        public string NotesRendered { get; set; }

        public DateTime MetadataModifiedAsDate
        {
            get
            {
                return DateHelper.Parse(MetadataModified);
            }
        }

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
            summary = Regex.Replace(summary, @"<(.|\n)*?>", "");

            // Abbreviate the notes
            if (summary.Length > length)
            {
                // Find the nearest word and crop the string there.
                summary = summary.Substring(0, summary.IndexOf(" ", length-5)) + "...";
            }
            return summary;
        }
    }

}
