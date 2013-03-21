using CkanDotNet.Api.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Model
{
    public class Related
    {
        /// <summary>
        /// Gets the auto-generated unique identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets the auto-generated unique identifier of the owner
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// Gets the view count
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// Gets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets the URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets the featured value
        /// </summary>
        public int Featured { get; set; }

        /// <summary>
        /// Gets the image URL
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets the type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets the created date in ISO string format
        /// </summary>
        public string Created { get; set; }

        /// <summary>
        /// Gets the created date
        /// </summary>
        public DateTime CreatedAsDate
        {
            get
            {
                return DateHelper.Parse(Created);
            }
        }

    }
}
