using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CkanDotNet.Api.Model;
using System.Configuration;
using CkanDotNet.Web.Models.Settings;

namespace CkanDotNet.Web.Models.Helpers
{
    public static class SettingsHelper
    {
        #region Offline Settings

        /// <summary>
        /// Is the data catalog currently offline
        /// </summary>
        /// <returns></returns>
        public static bool GetOfflineEnabled()
        {
            string offline = ConfigurationManager.AppSettings["Offline.Enabled"];
            return (offline == "true") ? true : false;
        }

        /// <summary>
        /// Get the offline title
        /// </summary>
        /// <returns></returns>
        public static string GetOfflineTitle()
        {
            return ConfigurationManager.AppSettings["Offline.Title"];
        }

        /// <summary>
        /// Get the offline message
        /// </summary>
        /// <returns></returns>
        public static string GetOfflineMessage()
        {
            return ConfigurationManager.AppSettings["Offline.Message"];
        }

        #endregion

        #region Repository Settings

        /// <summary>
        /// Get the list of groups that have been configured in the settings.
        /// </summary>
        /// <returns></returns>
        public static string GetRepositoryHost()
        {
            return ConfigurationManager.AppSettings["Repository.Host"];
        }

        /// <summary>
        /// Get the respository request timeout in milliseconds.
        /// </summary>
        /// <returns></returns>
        public static int GetRepositoryRequestTimeout()
        {
            string timeout = ConfigurationManager.AppSettings["Repository.RequestTimeout"];
            return int.Parse(timeout);
        }

        #endregion

        #region Google Analytics Settings

        /// <summary>
        /// Is Google Analytics enabled
        /// </summary>
        /// <returns></returns>
        public static bool GetGoogleAnalyticsEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["GoogleAnalytics.Enabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Gets the Google Analytics profile
        /// </summary>
        /// <returns></returns>
        public static string GetGoogleAnalyticsProfile()
        {
            return ConfigurationManager.AppSettings["GoogleAnalytics.Profile"];
        }

        /// <summary>
        /// Gets the Google Analytics Domain Name
        /// </summary>
        /// <returns></returns>
        public static string GetGoogleAnalyticsDomainName()
        {
            return ConfigurationManager.AppSettings["GoogleAnalytics.DomainName"];
        }

        /// <summary>
        /// Is Google Analytics linker allowed
        /// </summary>
        /// <returns></returns>
        public static bool GetGoogleAnalyticsLinkerAllowed()
        {
            string allow = ConfigurationManager.AppSettings["GoogleAnalytics.AllowLinker"];
            return (allow == "true") ? true : false;
        }

        #endregion

        #region Catalog Settings

        /// <summary>
        /// Gets the current theme name
        /// </summary>
        /// <returns></returns>
        public static string GetCatalogTheme()
        {
            return ConfigurationManager.AppSettings["Catalog.Theme"];
        }

        /// <summary>
        /// Get the list of groups that have been configured in the settings.
        /// </summary>
        /// <returns></returns>
        public static string GetCatalogGroup()
        {
            return ConfigurationManager.AppSettings["Catalog.Group"];
        }

        /// <summary>
        /// Get the prefix for package titles.  The prefix is removed from 
        /// package names as the prefix is not necessary in a filtered catalog.
        /// </summary>
        /// <returns></returns>
        public static string GetCatalogPackageTitlePrefix()
        {
            return ConfigurationManager.AppSettings["Catalog.PackageTitlePrefix"];
        }

        /// <summary>
        /// Filter the package title with the package title prefix.
        /// </summary>
        /// <param name="package"></param>
        public static void FilterTitle(Package package)
        {
            string title = package.Title;
            string titlePrefix = GetCatalogPackageTitlePrefix();
            if (!String.IsNullOrEmpty(titlePrefix))
            {
                package.Title = title.Replace(titlePrefix, "");
            }

        }

        /// <summary>
        /// Filter the package titles with the package title prefix.
        /// </summary>
        /// <param name="packages"></param>
        public static void FilterTitles(List<Package> packages)
        {
            foreach (var package in packages)
            {
                FilterTitle(package);
            }
        }

        /// <summary>
        /// Get the list of hidden tags that have been configured in the settings
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCatalogHiddenTags()
        {
            return GetList("Catalog.HiddenTags");
        }

        /// <summary>
        /// Filter the package titles with the package title prefix.
        /// </summary>
        /// <param name="packages"></param>
        public static List<string> FilterTags(List<string> tags)
        {
            List<string> hiddenTags = GetCatalogHiddenTags();
            foreach (var hiddenTag in hiddenTags)
            {
                tags.Remove(hiddenTag);
            }
            return tags;
        }

        #endregion

        #region Resource Settings

        /// <summary>
        /// Get the resource settings (custom actions for CKAN resource types)
        /// </summary>
        /// <returns></returns>
        public static ResourceSettings GetResourceSettings()
        {
            ResourceSettings settings = new ResourceSettings();

            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                if (key.Contains("ResourceType"))
                {

                    // Get or create the resource type
                    string type = key.Split(char.Parse("."))[1];
                    string property = key.Split(char.Parse("."))[2];

                    ResourceType resourceType;
                    if (settings.Types.ContainsKey(type))
                    {
                        resourceType = settings.Types[type];
                    }
                    else
                    {
                        resourceType = new ResourceType();
                        settings.Types.Add(type, resourceType);
                    }

                    // Check for the title
                    if (property == "Title")
                    {
                        resourceType.Title = ConfigurationManager.AppSettings[key];
                    }
                    else if (property == "Actions")
                    {

                        // Add the actions
                        string value = ConfigurationManager.AppSettings[key];
                        value = value.Replace(Environment.NewLine, "");

                        foreach (var actionSetting in value.Split(char.Parse("|")))
                        {

                            ResourceAction action = new ResourceAction();

                            foreach (var parameterSetting in actionSetting.Split(char.Parse(";")))
                            {
                                string parameter = parameterSetting.Trim();

                                int equalsIndex = parameter.IndexOf("=");

                                var parameterKey = parameter.Substring(0, equalsIndex);
                                var parameterValue = parameter.Substring(equalsIndex + 1);

                                if (parameterKey == "action")
                                {
                                    action.Action = parameterValue;
                                }
                                else if (parameterKey == "url")
                                {
                                    action.UrlTemplate = parameterValue;
                                }
                                else if (parameterKey == "title")
                                {
                                    action.Title = parameterValue;
                                }

                            }

                            resourceType.Actions.Add(action);
                        }
                    }

                }
            }

            return settings;
        }

        #endregion

        #region Home Page Settings

        /// <summary>
        /// Gets the page title for the home page
        /// </summary>
        /// <returns></returns>
        public static string GetHomePageTitle()
        {
            return ConfigurationManager.AppSettings["Home.Title"];
        }

        /// <summary>
        /// Should popular tags be displayed on the home page
        /// </summary>
        /// <returns></returns>
        public static bool GetHomePopularTagsEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Home.PopularTagsEnabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Get the maximum number of popular tags to display on the home page
        /// </summary>
        /// <returns></returns>
        public static int GetHomePopularTagsLimit()
        {
            string limit = ConfigurationManager.AppSettings["Home.PopularTagsLimit"];
            return int.Parse(limit);
        }

        /// <summary>
        /// Should featured packages be displayed on the home page
        /// </summary>
        /// <returns></returns>
        public static bool GetHomeFeaturedPackagesEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Home.FeaturedPackagesEnabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Gets the tag to use for featured packages
        /// </summary>
        /// <returns></returns>
        public static string GetHomeFeaturedPackagesTag()
        {
            string tag = ConfigurationManager.AppSettings["Home.FeaturedPackagesTag"];
            return tag;
        }

        /// <summary>
        /// Get the maximum number of featured packages to display on the home page
        /// </summary>
        /// <returns></returns>
        public static int GetHomeFeaturedPackageLimit()
        {
            string limit = ConfigurationManager.AppSettings["Home.FeaturedPackagesLimit"];
            return int.Parse(limit);
        }

        /// <summary>
        /// Should recently updated packages be displayed on the home page
        /// </summary>
        /// <returns></returns>
        public static bool GetHomeRecentlyUpdatedPackagesEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Home.RecentlyUpdatedPackagesEnabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Should browsing of all packages be displayed on the home page
        /// </summary>
        /// <returns></returns>
        public static bool GetHomeBrowseAllPackagesEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Home.BrowseAllPackagesEnabled"];
            return (enabled == "true") ? true : false;
        }

        #endregion

        #region Search Page Settings

        /// <summary>
        /// Gets the page title for the search page
        /// </summary>
        /// <returns></returns>
        public static string GetSearchPageTitle()
        {
            return ConfigurationManager.AppSettings["Search.Title"];
        }

        /// <summary>
        /// Get the list of groups that have been configured in the settings.
        /// </summary>
        /// <returns></returns>
        public static int GetSearchResultsPerPage()
        {
            string results = ConfigurationManager.AppSettings["Search.ResultsPerPage"];
            return int.Parse(results);
        }

        /// <summary>
        /// Is the rating shown in the search results
        /// </summary>
        /// <returns></returns>
        public static bool GetSearchResultsShowRating()
        {
            string enabled = ConfigurationManager.AppSettings["Search.ResultsShowRating"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Are the tags shown in the search results
        /// </summary>
        /// <returns></returns>
        public static bool GetSearchResultsShowTags()
        {
            string enabled = ConfigurationManager.AppSettings["Search.ResultsShowTags"];
            return (enabled == "true") ? true : false;
        }

        #endregion

        #region Package Page Settings

        /// <summary>
        /// Gets the page title for the package page
        /// </summary>
        /// <returns></returns>
        public static string GetPackagePageTitle()
        {
            return ConfigurationManager.AppSettings["Package.Title"];
        }

        /// <summary>
        /// Get the list of groups that have been configured in the settings.
        /// </summary>
        /// <returns></returns>
        public static bool GetPackageRssFeedEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Package.RSSFeedEnabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Get the list of groups that have been configured in the settings.
        /// </summary>
        /// <returns></returns>
        public static int GetPackageRssFeedDays()
        {
            string days = ConfigurationManager.AppSettings["Package.RSSFeedDays"];
            return int.Parse(days);
        }

        /// <summary>
        /// Get the label for a package 'extra' field.
        /// </summary>
        /// <returns></returns>
        public static string GetPackageExtraFieldLabel(string field)
        {
            string label = ConfigurationManager.AppSettings[String.Format("Package.Extra.{0}", field)];
            if (String.IsNullOrEmpty(label))
            {
                label = field;
            }

            return label;
        }

        #endregion

        #region Cache Settings

        /// <summary>
        /// Gets the key required for administering the cache
        /// </summary>
        /// <returns></returns>
        public static string GetCacheAdminToken()
        {
            return ConfigurationManager.AppSettings["Cache.Admin.Token"];
        }

        /// <summary>
        /// Get the duration to cache the full package list.
        /// </summary>
        /// <returns></returns>
        public static TimeSpan GetAllPackagesCacheDuration()
        {
            string minutes = ConfigurationManager.AppSettings["Cache.AllPackages.Duration"];
            return TimeSpan.FromMinutes(int.Parse(minutes));
        }

        /// <summary>
        /// Automatically update the all packages cache in the background
        /// </summary>
        /// <returns></returns>
        public static bool GetAllPackagesCacheBackgroundUpdate()
        {
            string enabled = ConfigurationManager.AppSettings["Cache.AllPackages.BackgroundUpdate"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Get the duration to cache search results.
        /// </summary>
        /// <returns></returns>
        public static TimeSpan GetSearchResultsCacheDuration()
        {
            string minutes = ConfigurationManager.AppSettings["Cache.SearchResults.Duration"];
            return TimeSpan.FromMinutes(int.Parse(minutes));
        }

        /// <summary>
        /// Get the duration to cache package details.
        /// </summary>
        /// <returns></returns>
        public static TimeSpan GetPackageCacheDuration()
        {
            string minutes = ConfigurationManager.AppSettings["Cache.Package.Duration"];
            return TimeSpan.FromMinutes(int.Parse(minutes));
        }

        /// <summary>
        /// Get the duration to cache the pacakge count
        /// </summary>
        /// <returns></returns>
        public static TimeSpan GetPackageCountCacheDuration()
        {
            string minutes = ConfigurationManager.AppSettings["Cache.PackageCount.Duration"];
            return TimeSpan.FromMinutes(int.Parse(minutes));
        }

        /// <summary>
        /// Automatically update the package count cache in the background
        /// </summary>
        /// <returns></returns>
        public static bool GetPackageCountCacheBackgroundUpdate()
        {
            string enabled = ConfigurationManager.AppSettings["Cache.PackageCount.BackgroundUpdate"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Get the duration to cache featured packages.
        /// </summary>
        /// <returns></returns>
        public static TimeSpan GetFeaturedPackagesCacheDuration()
        {
            string minutes = ConfigurationManager.AppSettings["Cache.FeaturedPackages.Duration"];
            return TimeSpan.FromMinutes(int.Parse(minutes));
        }

        /// <summary>
        /// Get the duration to cache information about all licenses.
        /// </summary>
        /// <returns></returns>
        public static TimeSpan GetAllLicensesCacheDuration()
        {
            string minutes = ConfigurationManager.AppSettings["Cache.AllLicenses.Duration"];
            return TimeSpan.FromMinutes(int.Parse(minutes));
        }

        /// <summary>
        /// Automatically update the all licenses cache in the background
        /// </summary>
        /// <returns></returns>
        public static bool GetAllLicensesCacheBackgroundUpdate()
        {
            string enabled = ConfigurationManager.AppSettings["Cache.AllLicenses.BackgroundUpdate"];
            return (enabled == "true") ? true : false;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Get a list of comma delimited items from the a key in the settings
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static List<string> GetList(string key)
        {
            List<string> items = new List<string>();

            string itemsString = ConfigurationManager.AppSettings[key];
            if (!String.IsNullOrEmpty(itemsString))
            {
                char[] delimiter = new char[] { ',' };
                string[] itemsArray = itemsString.Split(delimiter);

                foreach (var item in itemsArray)
                {
                    items.Add(item.Trim());
                }

            }

            return items;
        }

        #endregion
    }
}