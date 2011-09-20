using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CkanDotNet.Web.Models.SiteMap;
using System.Web.Mvc;

namespace CkanDotNet.Web.Models.Helpers
{
    public static class SiteMapHelper
    {
        public static List<SiteMapUrl> GetSiteMapUrls(ControllerContext context)
        {
            var urlSet = new List<SiteMapUrl>();

            // Populate the URL set
            var packages = CkanHelper.GetAllPackages();
           
            // Get the base url
            UrlHelper helper = new UrlHelper(context.RequestContext);

            var request = context.RequestContext.HttpContext.Request;

            // TODO: Allow this to overridden in settings to support a reverse proxy
            var baseUrl = SettingsHelper.GetSeoSiteMapBaseUrl();
            if (String.IsNullOrEmpty(baseUrl))
            {
                baseUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath;
            }
            var baseUri = new Uri(baseUrl);

            AddHomepageToSiteMap(urlSet, packages, baseUri);

            AddPackagesToSiteMap(urlSet, packages, helper, baseUri);

            return urlSet;
        }

        /// <summary>
        /// Add the home page to the sitemap
        /// </summary>
        /// <param name="urlSet"></param>
        /// <param name="packages"></param>
        /// <param name="baseUrl"></param>
        private static void AddHomepageToSiteMap(List<SiteMapUrl> urlSet, List<Api.Model.Package> packages, Uri baseUrl)
        {
            // Add a URL for the home page
            SiteMapUrl homePageUrl = new SiteMapUrl();
            homePageUrl.Location = baseUrl.AbsoluteUri;
            homePageUrl.Priority = 1;
            homePageUrl.LastModification = DateTime.Now;

            // Get the most recently updated package to get the actual last mod time
            // of the home page
            var homePageLastUpdated = DateTime.Now;
            var recentlyUpdatePackages = (from package in packages
                                          orderby package.MetadataModified
                                          descending
                                          select package)
                                     .Take(1)
                                     .ToList();

            if (recentlyUpdatePackages.Count > 0)
            {
                homePageUrl.LastModification = recentlyUpdatePackages[0].MetadataModifiedAsDate;
            }

            homePageUrl.ChangeFrequency = GetChangeFrequency(homePageUrl.LastModification);

            urlSet.Add(homePageUrl);
        }

        /// <summary>
        /// Add the package list to the sitemap.
        /// </summary>
        /// <param name="urlSet"></param>
        /// <param name="packages"></param>
        /// <param name="helper"></param>
        /// <param name="baseUrl"></param>
        private static void AddPackagesToSiteMap(List<SiteMapUrl> urlSet, List<Api.Model.Package> packages, UrlHelper helper, Uri baseUrl)
        {
            // Build the sitemap for each package
            foreach (var package in packages)
            {
                var routeUrl = helper.Action("Index", "Package", new { package = package.Name });
                SiteMapUrl url = new SiteMapUrl();
                url.Location = new Uri(baseUrl, routeUrl).AbsoluteUri;
                url.LastModification = package.MetadataModifiedAsDate;
                url.ChangeFrequency = GetChangeFrequency(url.LastModification);
                url.Priority = 0.5;
                urlSet.Add(url);
            }
        }

        /// <summary>
        /// Get the estimated change frequency based on the last update date.
        /// </summary>
        /// <param name="lastModified"></param>
        /// <returns></returns>
        private static SiteMapUrlChangeFrequency GetChangeFrequency(DateTime lastModified)
        {
            SiteMapUrlChangeFrequency frequency = SiteMapUrlChangeFrequency.Never;

            if (lastModified.AddMonths(1) > DateTime.Now)
            {
                frequency = SiteMapUrlChangeFrequency.Weekly;
            }
            else if (lastModified.AddYears(1) > DateTime.Now)
            {
                frequency = SiteMapUrlChangeFrequency.Monthly;
            }
            else
            {
                frequency = SiteMapUrlChangeFrequency.Yearly;
            }

            return frequency;
        }

    }
}