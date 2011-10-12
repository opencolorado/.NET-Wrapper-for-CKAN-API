using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CkanDotNet.Api.Helper;

namespace CkanDotNet.Api.Model
{
    public class Group
    {
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
        /// Gets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the created date in ISO string format
        /// </summary>
        public string Created { get; set; }

        /// <summary>
        /// Gets the state of the group
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets the id of the most recent revision
        /// </summary>
        public string RevisionId { get; set; }

        /// <summary>
        /// Gets the packages in the group
        /// </summary>
        public List<string> Packages { get; set; }

        /// <summary>
        /// Gets the created date of the group
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
