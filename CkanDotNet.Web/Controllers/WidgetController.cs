using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CkanDotNet.Api.Model;
using CkanDotNet.Api;
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
        public ActionResult PopularTags ()
        {
            log.DebugFormat("Controller action requested");

            // Get the tag limit
            int tagLimit = SettingsHelper.GetHomePopularTagsLimit();

            // Collect the results
            List<Tag> tags = CkanHelper.GetAllTags(tagLimit);

            // Compute the tag scale
            tags = TagHelper.ComputeTagScale(tags);
            
            // Render the view
            return View(tags);
        }

        /// <summary>
        /// Provides a view of features packages in the repository group.  Featured
        /// packages are those that are tagged with "featured".
        /// </summary>
        public ActionResult FeaturedPackages()
        {
            log.DebugFormat("Controller action requested");

            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.Organizations.Add(SettingsHelper.GetCatalogGroup());
            searchParameters.Limit = SettingsHelper.GetHomeFeaturedPackageLimit();
            searchParameters.Tags.Add(SettingsHelper.GetHomeFeaturedPackagesTag());

            // Collect the results
            PackageSearchResponse<Package> results = CkanHelper.GetClient().SearchPackages<Package>(searchParameters, new CacheSettings(SettingsHelper.GetFeaturedPackagesCacheDuration()));
            SettingsHelper.FilterTitles(results.Results);

            // Render the view
            return View(results);
        }

        /// <summary>
        /// Provides a view of features packages that have recently bee updated in the repository group.
        /// Return the three most recently updated packages in the group.
        /// </summary>
        public ActionResult RecentlyUpdated()
        {
            log.DebugFormat("Controller action requested");

            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.Organizations.Add(SettingsHelper.GetCatalogGroup());

            // Collect the results
            List<Package> packages = CkanHelper.GetAllPackages();

            // Sort by date and take the top 3
            packages = (from package in packages
                                      orderby package.MetadataModified
                                      descending
                                      select package)
                         .Take(3)
                         .ToList();

            SettingsHelper.FilterTitles(packages);

            // Update the results to the top 3
            PackageSearchResponse<Package> response = new PackageSearchResponse<Package>();
            response.Results = packages;
            response.Count = packages.Count;

            // Populate the model
            PackageSearchResultsModel model = new PackageSearchResultsModel();
            model.SearchResults = response;

            // Render the view
            return View(model);
        }

        /// <summary>
        /// Provides a view of features packages that have recently bee updated in the repository group.
        /// Return the three most recently updated packages in the group.
        /// </summary>
        public ActionResult BrowsePackages()
        {
            log.DebugFormat("Controller action requested");

            var searchParameters = new PackageSearchParameters();
            searchParameters.Organizations.Add(SettingsHelper.GetCatalogGroup());

            // Set up the pagination
            Pager pager = new Pager(1, SettingsHelper.GetSearchResultsPerPage());
            searchParameters.Offset = pager.RecordOffset;
            searchParameters.Limit = pager.RecordsPerPage;

            PackageSearchResultsModel model = new PackageSearchResultsModel();
            model.SearchParameters = searchParameters;
            model.SearchResults = CkanHelper.GetClient().SearchPackages<Package>(searchParameters, new CacheSettings(SettingsHelper.GetSearchResultsCacheDuration()));
            SettingsHelper.FilterTitles(model.SearchResults.Results);
            model.Pager = pager;

            // Set the number of records to be paged
            pager.RecordCount = model.SearchResults.Count;

            // Render the view
            return View(model);
        }

        /// <summary>
        /// Provides a view of the top 10 tags in the repository group
        /// </summary>
        public ActionResult PackageCount()
        {
            return Content(CkanHelper.GetPackageCount().ToString());
        }
    }
}
