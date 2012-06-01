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

        private List<Resource> resources = new List<Resource>();

        private List<string> groups = new List<string>();

        private List<string> tags = new List<string>();

        private Dictionary<string,string> extras = new Dictionary<string,string>();

        private List<Package> relationships = new List<Package>();

        private string notesRendered;

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
        public List<string> Tags {
            get
            {
                return tags;
            }
            set
            {
                tags = value;
            }
        }

        /// <summary>
        /// Gets the groups associated with the package
        /// </summary>
        public List<string> Groups
        {
            get
            {
                return groups;
            }
            set
            {
                groups = value;
            }
        }

        /// <summary>
        /// Get the extra fields associated with the package
        /// </summary>
        public Dictionary<string, string> Extras {
            get
            {
                return extras;
            }
            set
            {
                extras = value;
            }        
        }

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
        public List<Resource> Resources {
            get
            {
                // If there is no resource collection and there
                // are resource urls, formats or descriptions
                // use that information to build the resource collection
                // This is required because the CKAN search result returns
                // a slightly different format that the REST package result.
                // This normalizes the format of a package so that the object
                // format is consistent.
                if (resources.Count == 0 && 
                    (ResFormat != null || ResUrl != null || ResDescription != null))
                {
                    for (int i = 0; i < ResFormat.Count; i++)
                    {
                        Resource resource = new Resource();
                        resource.Position = i.ToString(); 
                        resource.Format = ResFormat[i];
                        resource.Url = ResUrl[i];
                        resource.Description = ResDescription[i];
                        resource.PackageId = this.Id;
                        resources.Add(resource);
                    }
                }
                return resources;
            }
            set
            {
                resources = value;
            }
        }

        /// <summary>
        /// Gets the list of resource formats.
        /// Note: This property is here to support new versions of the CKAN 
        /// API but should not be used directly.  Use Resources property
        /// instead.
        /// </summary>
        public List<String> ResFormat { get; set; }

        /// <summary>
        /// Gets the list of resource descriptions
        /// Note: This property is here to support new versions of the CKAN 
        /// API but should not be used directly.  Use Resources property
        /// instead.
        /// </summary>
        public List<String> ResDescription { get; set; }

        /// <summary>
        /// Gets the list of resource urls
        /// Note: This property is here to support new versions of the CKAN 
        /// API but should not be used directly.  Use Resources property
        /// instead.
        /// </summary>
        public List<String> ResUrl { get; set; }

        /// <summary>
        /// Gets the URL to the package in the CKAN repository
        /// </summary>
        public string CkanUrl { get; set; }

        /// <summary>
        /// Gets the package relationships
        /// </summary>
        public List<Package> Relationships {
            get
            {
                return relationships;
            }
            set
            {
                relationships = value;
            }        
        }

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
        public string NotesRendered
        {
            get
            {
                if (String.IsNullOrEmpty(notesRendered)) {
                    notesRendered = Notes;
                }
                return notesRendered;
            }
            set
            {
                notesRendered = value;
            }
        
        }

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

            if (!String.IsNullOrEmpty(summary))
            {
                // Strip the HTML
                summary = Regex.Replace(summary, @"<(.|\n)*?>", "").Trim();

                // Abbreviate the notes
                if (summary.Length > length)
                {
                    // Get the nearest space
                    int spaceIndex = summary.IndexOf(" ", length);

                    if (spaceIndex > 0)
                    {
                        summary = summary.Substring(0, spaceIndex) + "...";
                    }
                    else
                    {
                        summary = summary.Substring(0, length) + "...";
                    }
                }
            }
            return summary;
        }
    }

}
