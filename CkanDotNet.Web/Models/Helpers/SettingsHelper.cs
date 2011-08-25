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
        /// <summary>
        /// Get the list of groups that have been configured in the settings.
        /// </summary>
        /// <returns></returns>
        public static string GetRepository()
        {
            return ConfigurationManager.AppSettings["Repository"];
        }

        /// <summary>
        /// Gets the current theme name
        /// </summary>
        /// <returns></returns>
        public static string GetTheme()
        {
            return ConfigurationManager.AppSettings["Theme"];
        }

        /// <summary>
        /// Gets the page title for the home page
        /// </summary>
        /// <returns></returns>
        public static string GetHomePageTitle()
        {
            return ConfigurationManager.AppSettings["Home.Title"];
        }

        /// <summary>
        /// Gets the page title for the search page
        /// </summary>
        /// <returns></returns>
        public static string GetSearchPageTitle()
        {
            return ConfigurationManager.AppSettings["Search.Title"];
        }

        /// <summary>
        /// Gets the page title for the package page
        /// </summary>
        /// <returns></returns>
        public static string GetPackagePageTitle()
        {
            return ConfigurationManager.AppSettings["Package.Title"];
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
        /// Should browsing of all packages be displayed on the home page
        /// </summary>
        /// <returns></returns>
        public static bool GetHomeBrowseAllPackagesEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Home.BrowseAllPackagesEnabled"];
            return (enabled == "true") ? true : false;
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

        /// <summary>
        /// Get the list of groups that have been configured in the settings.
        /// </summary>
        /// <returns></returns>
        public static string GetGroup()
        {
            return ConfigurationManager.AppSettings["Group"];
        }

        /// <summary>
        /// Get the list of hidden tags that have been configured in the settings
        /// </summary>
        /// <returns></returns>
        public static List<string> GetHiddenTags()
        {
            return GetList("HiddenTags");
        }

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

        /// <summary>
        /// Filter the package title with the package title prefix.
        /// </summary>
        /// <param name="package"></param>
        public static void FilterTitle(Package package)
        {
            string title = package.Title;
            string titlePrefix = ConfigurationManager.AppSettings["PackageTitlePrefix"];
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
        /// Filter the package titles with the package title prefix.
        /// </summary>
        /// <param name="packages"></param>
        public static List<string> FilterTags(List<string> tags)
        {
            List<string> hiddenTags = GetHiddenTags();
            foreach (var hiddenTag in hiddenTags)
            {
                tags.Remove(hiddenTag);
            }
            return tags;
        }

        public static ResourceSettings GetResourceSettings()
        {
            ResourceSettings settings = new ResourceSettings();

            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                if (key.Contains("ResourceType")) {

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
                        settings.Types.Add(type,resourceType);
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
    }
}