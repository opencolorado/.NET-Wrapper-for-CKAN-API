using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using CkanDotNet.Api;
using CkanDotNet.Web.Models;
using CkanDotNet.Api.Model;
using log4net;
using System.Reflection;
using System.Configuration;
using CkanDotNet.Web.Models.Helpers;

namespace CkanDotNet.Web.Controllers
{
    public class SearchController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Default search action
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [CheckOffline]
        
        public ActionResult Index(string q, int? page, string order_by, string tag)
        {
            log.DebugFormat("Controller action requested");

            this.ConfigureBreadCrumbs(q, tag);

            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.Query = q;
            searchParameters.Groups.Add(SettingsHelper.GetCatalogGroup());
            
            // Ordering
            if (!String.IsNullOrEmpty(order_by))
            {
                searchParameters.OrderBy = order_by;
            }

            // Tag
            if (!String.IsNullOrEmpty(tag))
            {
                searchParameters.Tags.Add(tag);
            }

            // Set up the pagination
            int pageNumber = 1;
            if (page != null)
            {
                pageNumber = (int)page;
            }

            Pager pager = new Pager(pageNumber, SettingsHelper.GetSearchResultsPerPage());
            searchParameters.Offset = pager.RecordOffset;
            searchParameters.Limit = pager.RecordsPerPage;

            // Build the view model for the results
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
        /// Configure the breadcrumbs for this controller.
        /// </summary>
        /// <param name="q">The query</param>
        /// <param name="tag">The selected tag</param>
        private void ConfigureBreadCrumbs(string q, string tag)
        {
            // Set up the breadcrumbs for this action
            var breadCrumbs = new BreadCrumbs();

            breadCrumbs.Add(new BreadCrumb(
                "Home",
                "Index",
                "Home"));

            // Add breadcrumb for the search action
            breadCrumbs.Add(new BreadCrumb(
                "Search"));


            // Add breadcrumb for the search filter (query)
            if (!String.IsNullOrEmpty(q))
            {
                var routeValues = RouteHelper.RouteFromParameters(RouteData.Values);
                // Create a route that removes the q parameter
                breadCrumbs.Add(new BreadCrumb(
                    String.Format("Query: {0}", q),
                    "Index",
                    RouteHelper.UpdateRoute(routeValues, "q", ""),
                    true)
                );
            }

            // Add breadcrumb for the tag filter
            if (!String.IsNullOrEmpty(tag))
            {
               var routeValues = RouteHelper.RouteFromParameters(RouteData.Values);
               // Create a route that removes the tag parameter
               breadCrumbs.Add(new BreadCrumb(
                   String.Format("Tag: {0}",tag),
                   "Index",
                   RouteHelper.UpdateRoute(routeValues, "tag", ""),
                   true)
               );
            }

            ViewData["BreadCrumbs"] = breadCrumbs;
        }
    }
}
