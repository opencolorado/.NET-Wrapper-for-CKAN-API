using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CkanDotNet.Api.Model;

namespace CkanDotNet.Web.Models
{
    public class PackageSearchResultsModel : SearchResultsSideBarModel
    {
        public PackageSearchResponse<Package> SearchResults { get; set; }
        public Pager Pager { get; set; }
        public PackageSearchParameters SearchParameters { get; set; } 
    }
}