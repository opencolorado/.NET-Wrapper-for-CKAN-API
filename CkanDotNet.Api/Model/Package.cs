using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Model
{
    public class Package
    {
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
        public PackageExtras Extras { get; set; }
        public double? RatingsAverage { get; set; }
        public int RatingsCount { get; set; }
        public List<Resource> Resources { get; set; }
        public string CkanUrl { get; set; }
        public List<Package> Relationships { get; set; }
        public DateTime MetadataModified { get; set; }
        public DateTime MetadataCreated { get; set; }
        public string NotesRendered { get; set; }

        public string GetAbbreviatedNotes(int length)
        {
            string summary = Notes;
            if (summary.Length > length)
            {
                summary = summary.Substring(0, length) + "...";
            }
            return summary;
        }

        public string BaseTitle
        {
            get
            {   
                //TODO: Move to config
                return Title.Replace("City of Denver: ","");
            }
        }
    }

}
