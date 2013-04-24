using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Model
{
    public class Resource
    {
        private string format;

        /// <summary>
        /// Gets the auto-generated unique identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets the auto-generated unique identifier for the resource group
        /// </summary>
        public string ResourceGroupId { get; set; }

        /// <summary>
        /// Gets the URL of the resource
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets the resource format
        /// </summary>
        public string Format
        {
            get
            {
                if (!String.IsNullOrEmpty(format)) {
                    format = format.ToLower();
                }
                return format;
            }
            set
            {
                format = value;
            }
        }

        /// <summary>
        /// Gets a description of the resource
        /// </summary>
        public string Description { get; set; }

        public string Hash { get; set; }
        public string Position { get; set; }
        public string PackageId { get; set; }
    }
}
