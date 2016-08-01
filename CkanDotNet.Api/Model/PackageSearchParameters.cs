﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Model
{
    /// <summary>
    /// Respresents the Package-Search-Params format in the CKAN REST API.
    /// </summary>
    public class PackageSearchParameters
    {
        private List<string> tags = new List<string>();
        private List<string> organizations = new List<string>();
        private string orderBy;
        private int offset = -1;
        private int limit = -1;

        /// <summary>
        /// Criteria to search all package fields for.  Used for general purpose searching.
        /// Search results must contain all the specified words.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Search the title field.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Search by tags.
        /// </summary>
        public List<string> Tags { get { return tags; }  }

        /// <summary>
        /// Search by groups.
        /// </summary>
        public List<string> Organizations { get { return organizations; } }

        /// <summary>
        /// Search the author field.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Search the maintainer field.
        /// </summary>
        public string Maintainer { get; set; }

        /// <summary>
        /// Search the notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Search by update frequency.
        /// </summary>
        public string UpdateFrequency { get; set; }

        /// <summary>
        /// Specify the field to sort the results by.  Defaults to "rank"
        /// </summary>
        public string OrderBy 
        {
            get { return orderBy; }
            set { orderBy = value; }
        }

        /// <summary>
        /// The page number to return when paginating.
        /// </summary>
        public int Offset
        {
            get { return offset; }
            set { offset = value; 
            }
        }

        /// <summary>
        /// The number of records to return per page.
        /// </summary>
        public int Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        /// <summary>
        /// Filters results by ones which are open (default: false)
        /// </summary>
        public bool FilterByOpenness { get; set; }

        /// <summary>
        /// Filters results by ones which have at least one resource URL (default: false)
        /// </summary>
        public bool FilterByDownloadable { get; set; }

        /// <summary>
        /// Aggregate tag counts.  Gets tags counts for all results (not just the current page) (default: false)
        /// </summary>
        public bool AggregateTagCounts { get; set; }

    }
}
