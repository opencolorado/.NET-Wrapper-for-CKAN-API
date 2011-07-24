using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Api.Model;
using CkanDotNet.Api;
using System.Web.UI;
using CkanDotNet.Web.Models;
using log4net;
using System.Reflection;
using CkanDotNet.Web.Models.Helpers;

namespace CkanDotNet.Web.Controllers
{
    public class WidgetController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Provides a view of the top 10 tags in the repository group
        /// </summary>
        [OutputCache(Duration = 300)]
        public ActionResult PopularTags ()
        {
            log.DebugFormat("Controller action requested");

            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.Groups.Add(SettingsHelper.GetGroup());

            // Collect the results
            PackageSearchResponse<Package> results = CkanHelper.GetClient().SearchPackages<Package>(searchParameters);
            SettingsHelper.FilterTitles(results.Results);

            // Get the tag counts
            List<string> ignoreTags = SettingsHelper.GetHiddenTags();
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
            log.DebugFormat("Controller action requested");

            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.Groups.Add(SettingsHelper.GetGroup());
            searchParameters.Limit = 5;
            searchParameters.Tags.Add("featured");

            // Collect the results
            PackageSearchResponse<Package> results = CkanHelper.GetClient().SearchPackages<Package>(searchParameters);
            SettingsHelper.FilterTitles(results.Results);

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
            log.DebugFormat("Controller action requested");

            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.Groups.Add(SettingsHelper.GetGroup());

            // Collect the results
            PackageSearchResponse<Package> results = CkanHelper.GetClient().SearchPackages<Package>(searchParameters);

            // Sort by date and take the top 3
            List<Package> packages = (from package in results.Results
                                      orderby package.MetadataModified
                                      descending
                                      select package)
                         .Take(3)
                         .ToList();

            SettingsHelper.FilterTitles(packages);

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
