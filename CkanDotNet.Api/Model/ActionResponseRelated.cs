using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Model
{
    public class ActionResponseRelated : ActionResponse
    {
        /// <summary>
        /// Gets the related items
        /// </summary>
        public List<Related> Result { get; set; }
    }
}
