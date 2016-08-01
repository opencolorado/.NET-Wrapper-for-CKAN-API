﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CkanDotNet.Api;
using CkanDotNet.Web.Models.Helpers;
using CkanDotNet.Api.Model;
using System.Web.Caching;

namespace CkanDotNet.Web.Models
{
    public static class CkanHelper
    {
        /// <summary>
        /// Gets a CKAN client for the configured repository.
        /// </summary>
        /// <returns></returns>
        public static CkanClient GetClient()
        {
            CkanClient client = new CkanClient(SettingsHelper.GetRepositoryHost());
            client.Timeout = SettingsHelper.GetRepositoryRequestTimeout();
            return client;
        }

        /// <summary>
        /// Get the number of packages in the CKAN group.
        /// </summary>
        /// <returns></returns>
        public static int GetPackageCount()
        {
            var searchParameters = new PackageSearchParameters();
            searchParameters.Organizations.Add(SettingsHelper.GetCatalogGroup());
            searchParameters.Limit = 0;

            int count = CkanHelper.GetClient().SearchPackages<string>(searchParameters, new CacheSettings(SettingsHelper.GetPackageCountCacheDuration(), SettingsHelper.GetPackageCountCacheBackgroundUpdate())).Count;
            return count;
        }

        /// <summary>
        /// Get all tags from the CKAN group.
        /// </summary>
        /// <returns></returns>
        public static List<Tag> GetAllTags(int limit)
        {
            CacheSettings settings = new CacheSettings(SettingsHelper.GetAllPackagesCacheDuration(), SettingsHelper.GetAllPackagesCacheBackgroundUpdate());

            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.Organizations.Add(SettingsHelper.GetCatalogGroup());

            // Collect the results
            List<Tag> tags = CkanHelper.GetClient().SearchPackagesGetTagCounts(searchParameters, settings);

            // Remove filtered tags
            tags = TagHelper.FilterTags(tags);

            if (limit > 0)
            {
                tags = TagHelper.LimitTags(tags, limit);
            }

            return tags;
        }


        /// <summary>
        /// Get all packages from the CKAN group.
        /// </summary>
        /// <returns></returns>
        public static List<Package> GetAllPackages()
        {
            CacheSettings settings = new CacheSettings(SettingsHelper.GetAllPackagesCacheDuration(), SettingsHelper.GetAllPackagesCacheBackgroundUpdate());

            // Create the CKAN search parameters
            var searchParameters = new PackageSearchParameters();
            searchParameters.Organizations.Add(SettingsHelper.GetCatalogGroup());
            searchParameters.Limit = GetPackageCount();

            // Collect the results
            PackageSearchResponse<Package> response = CkanHelper.GetClient().SearchPackages<Package>(searchParameters, settings);

            return response.Results;
        }

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
        /// TODO: Implement common caching proxy for cachable requests.
        /// </summary>
        /// <returns></returns>
        public static List<License> GetLicenses()
        {
            CacheSettings settings = new CacheSettings(SettingsHelper.GetAllLicensesCacheDuration(), SettingsHelper.GetAllLicensesCacheBackgroundUpdate());
            return CkanHelper.GetClient().GetLicenses(settings);
        }

        /// <summary>
        /// Get search suggestions for all packages in the catalog group.  Uses the all packages cache
        /// to build a search index that is also cached for performance.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static List<string> GetPackageSearchSuggestions(string prefix)
        {
            CacheSettings settings = new CacheSettings(SettingsHelper.GetAllPackagesCacheDuration(), SettingsHelper.GetAllPackagesCacheBackgroundUpdate());

            // Create the CKAN search parameters to get all packages in the group
            var searchParameters = new PackageSearchParameters();
            searchParameters.Organizations.Add(SettingsHelper.GetCatalogGroup());
            searchParameters.Limit = GetPackageCount();

            // Get a list of search suggestions
            List<string> suggestions = CkanHelper.GetClient().GetPackageSearchSuggestions(prefix, searchParameters, settings);

            return suggestions;
        }

    }
}