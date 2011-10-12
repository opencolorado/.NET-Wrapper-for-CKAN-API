using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CkanDotNet.Api.Helper;

namespace CkanDotNet.Api.Model
{
    public class License
    {
        /// <summary>
        /// Gets the status of the license
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets the maintainer of the license
        /// </summary>
        public string Maintainer { get; set; }

        /// <summary>
        /// Gets the license family
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// Gets the tags associated with the license
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets the URL for more information about the license
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets the title of the license to be displayed
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Is the license compliant with the Open Knowledge Definition
        /// </summary>
        public bool IsOkdCompliant { get; set; }

        /// <summary>
        /// Is the license compliant with the Open Source Initiative
        /// </summary>
        public bool IsOsiCompliant { get; set; }

        /// <summary>
        /// Gets the date created in ISO string format
        /// </summary>
        public string DateCreated { get; set; }

        /// <summary>
        /// Gets the auto-generated unique identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets the date created
        /// </summary>
        public DateTime DateCreatedAsDate
        {
            get
            {
                return DateHelper.Parse(DateCreated);
            }
        }
    }
}
