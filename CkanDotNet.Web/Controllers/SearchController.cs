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

namespace CkanDotNet.Web.Controllers
{
    public class SearchController : Controller
    {
        /// <summary>
        /// Default search action
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(string q, int? page, string order_by, string format, string tag)
        {
            this.ConfigureBreadCrumbs(q, tag, format);

            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.Query = q;
            searchParameters.IncludePackageDetails = true;
            
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

            if (!String.IsNullOrEmpty(format))
            {
                searchParameters.Format = format;
            }

            // Set up the pagination
            int pageNumber = 1;
            if (page != null)
            {
                pageNumber = (int)page;
            }

            Pager pager = new Pager(pageNumber, 10);
            searchParameters.Offset = pager.RecordOffset;
            searchParameters.Limit = pager.RecordsPerPage;

            // Build the view model for the results
            PackageSearchResultsModel model = ViewDataFactory.Create<PackageSearchResultsModel>();
            model.SearchParameters = searchParameters;
            model.SearchResults = Ckan.SearchPackages(searchParameters);
            model.Pager = pager;

            // Set the number of records to be paged
            pager.RecordCount = model.SearchResults.Count;            

            // Render the view
            return View(model);
        }

        private void ConfigureBreadCrumbs(string q, string tag, string format)
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

            // Add breadcrumb for the format filter
            if (!String.IsNullOrEmpty(format))
            {
                var routeValues = RouteHelper.RouteFromParameters(RouteData.Values);
                // Create a route that removes the format parameter
                breadCrumbs.Add(new BreadCrumb(
                    String.Format("Format: {0}", format),
                    "Index",
                    RouteHelper.UpdateRoute(routeValues, "format", ""),
                    true)
                );
            }

            ViewData["BreadCrumbs"] = breadCrumbs;
        }
    }
}
