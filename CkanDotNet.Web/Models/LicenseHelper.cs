using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CkanDotNet.Api.Model;
using CkanDotNet.Api;
using System.Web.Caching;

namespace CkanDotNet.Web.Models
{
    public static class LicenseHelper
    {
        /// <summary>
        /// Get a license by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static License GetLicenseById(string id)
        {
            License license = null;

            List<License> licenses = GetLicenses();
            foreach (var licenseEntry in licenses)
            {
                if (licenseEntry.Id == id)
                {
                    license = licenseEntry;
                    break;
                }
            }
            return license;
        }

        /// <summary>
        /// Get the licenses from the repository.  Caches
        /// the licenses for 1 hour as these don't update frequently.
        /// </summary>
        /// <returns></returns>
        private static List<License> GetLicenses()
        {
            List<License> licenses = null;

            if (HttpRuntime.Cache["Licenses"] != null)
            {
                licenses = (List<License>)HttpRuntime.Cache.Get("Licenses");
            }
            else
            {
                licenses = Ckan.GetLicenses();

                HttpRuntime.Cache.Insert(
                    "License",
                    licenses,
                    null,
                    Cache.NoAbsoluteExpiration,
                    new TimeSpan(1, 0, 0),
                    CacheItemPriority.Default,
                    null
                );
            }

            return licenses;
        }
    }
}