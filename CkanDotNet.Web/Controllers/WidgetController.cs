using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Api.Model;
using CkanDotNet.Api;
using System.Web.UI;
using CkanDotNet.Web.Models;

namespace CkanDotNet.Web.Controllers
{
    public class WidgetController : Controller
    {
        //
        // GET: /Widget/

        [OutputCache(Duration = 300)]
        public ActionResult PopularTags ()
        {
            var searchParameters = new PackageSearchParameters();
            searchParameters.IncludePackageDetails = true;

            PackageSearchResults results = Ckan.SearchPackages(searchParameters);

            List<string> ignoreTags = new List<string>();
            ignoreTags.Add("denver");
            ignoreTags.Add("colorado");
            Dictionary<string, int> tagCounts = TagHelper.GetTagCounts(results, ignoreTags, 10);
            
            return View(tagCounts);
        }

        [OutputCache(Duration = 300)]
        public ActionResult FeaturedPackages()
        {
            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.IncludePackageDetails = true;
            searchParameters.Limit = 5;
            searchParameters.Tags.Add("featured");

            PackageSearchResults results = Ckan.SearchPackages(searchParameters);

            return View(results);
        }

        [OutputCache(Duration = 300)]
        public ActionResult RecentlyUpdated()
        {
            var searchParameters = new PackageSearchParameters();
            searchParameters.IncludePackageDetails = true;

            PackageSearchResults results = Ckan.SearchPackages(searchParameters);

            List<Package> packages = (from package in results.Results
                                      orderby package.MetadataModified
                                      descending
                                      select package)
                         .Take(3)
                         .ToList();

            results.Results = packages;
            results.Count = packages.Count;

            PackageSearchResultsModel model = new PackageSearchResultsModel();
            model.SearchResults = results;

            return View(model);
        }
    }
}
