using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Model
{
    /// <summary>
    /// Respresents the Resource-Search-Params format in the CKAN REST API.
    /// </summary>
    public class ResourceSearchParameters
    {
        private int offset = 0;
        private int limit = 20;
        
        /// <summary>
        /// Search packages by resource url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Search packages by resource format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Search by resource description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Searches for a match of the hash field. An exact match or 
        /// match up to the length of the hash given.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// The page number to return when paginating.
        /// </summary>
        public int Offset
        {
            get { return offset; }
            set
            {
                offset = value;
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

    }
}
