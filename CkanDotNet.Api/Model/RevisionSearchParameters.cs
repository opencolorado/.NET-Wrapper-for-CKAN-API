using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Model
{
    /// <summary>
    /// Respresents the Resource-Search-Params format in the CKAN REST API.
    /// </summary>
    public class RevisionSearchParameters
    {
        /// <summary>
        /// Search packages revised since this time.
        /// </summary>
        public DateTime SinceTime { get; set; }

        /// <summary>
        /// Search packages revised since this id.  The stated id 
        /// will not be included in the results.
        /// </summary>
        public string SinceId { get; set; }
    }
}
