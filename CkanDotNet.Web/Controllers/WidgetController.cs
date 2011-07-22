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
        /// <summary>
        /// Provides a view of the top 10 tags in the repository group
        /// </summary>
        [OutputCache(Duration = 300)]
        public ActionResult PopularTags ()
        {
            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.Groups.Add("denver");
            searchParameters.IncludePackageDetails = true;

            // Collect the results
            PackageSearchResults results = CkanHelper.GetClient().SearchPackages(searchParameters);

            // Get the tag counts
            List<string> ignoreTags = new List<string>();
            ignoreTags.Add("denver");
            ignoreTags.Add("colorado");
            Dictionary<string, int> tagCounts = TagHelper.GetTagCounts(results, ignoreTags, 10);
            
            // Render the view
            return View(tagCounts);
        }

        /// <summary>
        /// Provides a view of features packages in the repository group.  Featured
        /// packages are those that are tagged with "featured".
        /// </summary>
        [OutputCache(Duration = 300)]
        public ActionResult FeaturedPackages()
        {
            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.Groups.Add("denver");
            searchParameters.IncludePackageDetails = true;
            searchParameters.Limit = 5;
            searchParameters.Tags.Add("featured");

            // Collect the results
            PackageSearchResults results = CkanHelper.GetClient().SearchPackages(searchParameters);

            // Render the view
            return View(results);
        }

        /// <summary>
        /// Provides a view of features packages that have recently bee updated in the repository group.
        /// Return the three most recently updated packages in the group.
        /// </summary>
        [OutputCache(Duration = 300)]
        public ActionResult RecentlyUpdated()
        {
            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.Groups.Add("denver");
            searchParameters.IncludePackageDetails = true;

            // Collect the results
            PackageSearchResults results = CkanHelper.GetClient().SearchPackages(searchParameters);

            // Sort by date and take the top 3
            List<Package> packages = (from package in results.Results
                                      orderby package.MetadataModified
                                      descending
                                      select package)
                         .Take(3)
                         .ToList();

            // Update the results to the top 3
            results.Results = packages;
            results.Count = packages.Count;

            // Populate the model
            PackageSearchResultsModel model = new PackageSearchResultsModel();
            model.SearchResults = results;

            // Render the view
            return View(model);
        }
    }
}
