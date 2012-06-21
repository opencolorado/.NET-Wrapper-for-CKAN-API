using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Api;
using CkanDotNet.Api.Model;
using CkanDotNet.Web.Models;
using log4net;
using System.Reflection;
using CkanDotNet.Web.Models.Helpers;

namespace CkanDotNet.Web.Controllers
{
    public class PackageController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //
        // GET: /Package/
        [CheckOffline]
        public ActionResult Index(string package)
        {
            log.DebugFormat("Controller action requested for package {0}", package);

            Package packageFound = CkanHelper.GetClient().GetPackage(package, new CacheSettings(SettingsHelper.GetPackageCacheDuration()));
            SettingsHelper.FilterTitle(packageFound);
            SettingsHelper.FilterTags(packageFound.Tags);

            if (package != null)
            {
                this.ConfigureBreadCrumbs(packageFound);
                this.ConfigureMetaTags(packageFound);
            }

            return View(packageFound);
        }

        /// <summary>
        /// Prepare the breadcrumbs model for this view.
        /// </summary>
        /// <param name="package"></param>
        private void ConfigureBreadCrumbs(Package package)
        {
            // Set up the breadcrumbs for this action
            var breadCrumbs = new BreadCrumbs();

            breadCrumbs.Add(new BreadCrumb(
                SettingsHelper.GetCatalogBreadcrumbsBaseLabel(),
                "Index",
                "Home"));

            breadCrumbs.Add(new BreadCrumb(
                package.Title));

            ViewData["BreadCrumbs"] = breadCrumbs;
        }


        /// <summary>
        /// Prepare the meta tags for this view.
        /// </summary>
        /// <param name="package">The package</param>
        private void ConfigureMetaTags(Package package)
        {
            var metaTags = new MetaTags();

            // Set the description from the package summary
            metaTags.Description = package.GetAbbreviatedNotes(SettingsHelper.GetSeoPackageDescriptionLength());

            // Get the common keywords from the settings
            var keywords = SettingsHelper.GetSeoCommonPackageKeywords();

            // Add the tags from the package
            keywords.AddRange(package.Tags);

            // Convert the list into a comma-delimited string
            var keywordsString = String.Join(",", keywords.ToArray());

            // Replace CKAN hyphens with spaces
            keywordsString = keywordsString.Replace("-", " ");

            // Set the page keywords
            metaTags.Keywords = keywordsString;
            
            ViewData["MetaTags"] = metaTags;
        }

    }
}
