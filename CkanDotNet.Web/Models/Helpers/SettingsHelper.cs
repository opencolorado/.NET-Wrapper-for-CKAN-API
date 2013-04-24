using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CkanDotNet.Api.Model;
using System.Configuration;
using CkanDotNet.Web.Models.Settings;
using CkanDotNet.Api.Helper;

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

        #region jQuery Settings

        /// <summary>
        /// Get the jQuery source
        /// </summary>
        /// <returns></returns>
        public static string GetJQuerySource()
        {
            return ConfigurationManager.AppSettings["jQuery.Source"];
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

        #region Suggestion Settings

        /// <summary>
        /// Are suggestions enabled
        /// </summary>
        /// <returns></returns>
        public static bool GetSuggestionsEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Suggestions.Enabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Set the delay time for queueing keystokes before 
        // requesting suggestions.
        /// </summary>
        /// <returns></returns>
        public static int GetSuggestionsDelay()
        {
            string delay = ConfigurationManager.AppSettings["Suggestions.Delay"];
            return int.Parse(delay);
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

        #region SEO Settings

        /// <summary>
        /// The meta tag description for the home page
        /// </summary>
        /// <returns></returns>
        public static string GetSeoHomeDescription()
        {
            return ConfigurationManager.AppSettings["SEO.HomeDescription"];
        }

        /// <summary>
        /// The meta tag keywords for the home page
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSeoHomeKeywords()
        {
            return GetKeywordsFromSetting("SEO.HomeKeywords");
        }

        /// <summary>
        /// Gets common package keywords to display in the meta keywords for every package page
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSeoCommonPackageKeywords()
        {
            return GetKeywordsFromSetting("SEO.CommonPackageKeywords");
        }

        /// <summary>
        /// Get comma-delimited keywords from a setting.
        /// </summary>
        /// <returns></returns>
        private static List<string> GetKeywordsFromSetting(string setting)
        {
            List<string> keywords = new List<string>();

            string keywordsString = ConfigurationManager.AppSettings["SEO.CommonPackageKeywords"];

            if (!String.IsNullOrEmpty(keywordsString))
            {
                var keywordsArray = keywordsString.Split(char.Parse(","));
                foreach (var keyword in keywordsArray)
                {
                    keywords.Add(keyword.Trim());
                }
            }
            return keywords;
        }

        /// <summary>
        /// Get the maximum number of characters to use in the package description meta tag.
        /// </summary>
        /// <returns></returns>
        public static int GetSeoPackageDescriptionLength()
        {
            string length = ConfigurationManager.AppSettings["SEO.PackageDescriptionLength"];
            return int.Parse(length);
        }

        /// <summary>
        /// The base map to use for the URLs in the sitemap.  This
        /// setting is used if the adminstrator wants to override 
        /// the default url (for example, if using a reverse proxy).
        /// </summary>
        /// <returns></returns>
        public static string GetSeoSiteMapBaseUrl()
        {
            return ConfigurationManager.AppSettings["SEO.SiteMapBaseUrl"];
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
        /// Gets the enabled status of the breadcrumbs
        /// </summary>
        /// <returns></returns>
        public static bool GetBreadcrumbsEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Catalog.BreadcrumbsEnabled"];
            return (enabled == "false") ? false : true;
        }

        /// <summary>
        /// Gets the enabled status of the breadcrumbs on the home page
        /// </summary>
        /// <returns></returns>
        public static bool GetBreadcrumbsHomeEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Catalog.BreadcrumbsHomeEnabled"];
            return (enabled == "false") ? false : true;
        }

        /// <summary>
        /// Gets the label to use for the base breadcrumb
        /// </summary>
        /// <returns></returns>
        public static string GetCatalogBreadcrumbsBaseLabel()
        {
            return ConfigurationManager.AppSettings["Catalog.BreadcrumbsBaseLabel"];
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
        /// Get the maximum length of a dataset title.
        /// </summary>
        /// <returns></returns>
        public static int GetDatasetTitleLength()
        {
            
            string configTitleLength = ConfigurationManager.AppSettings["Catalog.DatasetTitleLength"];
            int titleLength = 60;
            
            // Check if the app setting above returned a value.
            if (!string.IsNullOrEmpty(configTitleLength))
            {
                titleLength = int.Parse(configTitleLength);
            }

            return titleLength;
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

        #endregion


        #region Iframe Settings

        /// <summary>
        /// Gets the enabled status of enhanced iframe support
        /// </summary>
        /// <returns></returns>
        public static bool GetIframeEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Iframe.Enabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Get the document domain for the iframe.
        /// </summary>
        /// <returns></returns>
        public static string GetIframeDocumentDomain()
        {
            return ConfigurationManager.AppSettings["Iframe.DocumentDomain"];
        }

        /// <summary>
        /// Get the id of the iframe on the parent page.
        /// </summary>
        /// <returns></returns>
        public static string GetIframeId()
        {
            return ConfigurationManager.AppSettings["Iframe.Id"];
        }

        /// <summary>
        /// Get the source of the iframe on the parent page.
        /// </summary>
        /// <returns></returns>
        public static string GetIframeSrc()
        {
            return ConfigurationManager.AppSettings["Iframe.Src"];
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
                    string type = key.Split(char.Parse("."))[1].ToLower();
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
        /// Get the maximum number of search results to show per page.
        /// </summary>
        /// <returns></returns>
        public static int GetSearchResultsPerPage()
        {
            string results = ConfigurationManager.AppSettings["Search.ResultsPerPage"];
            return int.Parse(results);
        }

        /// <summary>
        /// Get the max number of pages to show in the pager.  A moving window around the current
        /// page will be used.
        /// </summary>
        /// <returns></returns>
        public static int GetSearchResultsMaxPagesToShowInPager()
        {
            string pagesSetting = ConfigurationManager.AppSettings["Search.ResultsMaxPagesToShowInPager"];
            int pages = 5; //Default
            if (!String.IsNullOrEmpty(pagesSetting))
            {
                int.TryParse(pagesSetting, out pages);
            } 
            return pages;
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
        /// Is the package related items section enabled
        /// </summary>
        /// <returns></returns>
        public static bool GetPackageRelatedItemsEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Package.RelatedItemsEnabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Is the package RSS feed link enabled
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

        /// <summary>
        /// Use the license block from the theme license page (_License).  Uses the 
        /// standard CKAN license information by default.
        /// </summary>
        /// <returns></returns>
        public static bool GetPackageUseThemeLicenseTemplate()
        {
            string enabled = ConfigurationManager.AppSettings["Package.UseThemeLicenseTemplate"];
            return (enabled == "true") ? true : false;
        }

        #endregion

        #region Tag Cloud Settings

        /// <summary>
        /// Should the count be displayed beside the tag
        /// </summary>
        /// <returns></returns>
        public static bool GetTagCloudShowCount()
        {
            string showCount = ConfigurationManager.AppSettings["TagCloud.ShowCount"];
            return (showCount == "true") ? true : false;
        }

        /// <summary>
        /// The minimum scale for the tag cloud (percent)
        /// </summary>
        /// <returns></returns>
        public static int GetTagCloudMinScale()
        {
            string scale = ConfigurationManager.AppSettings["TagCloud.MinScale"];
            return int.Parse(scale);
        }

        /// <summary>
        /// The maximum scale for the tag cloud
        /// </summary>
        /// <returns></returns>
        public static int GetTagCloudMaxScale()
        {
            string scale = ConfigurationManager.AppSettings["TagCloud.MaxScale"];
            return int.Parse(scale);
        }

        #endregion

        #region UserVoice Settings

        /// <summary>
        /// Are UserVoice suggestions enabled
        /// </summary>
        /// <returns></returns>
        public static bool GetUserVoiceEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["UserVoice.Enabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Get the UserVoice widget key
        /// </summary>
        /// <returns></returns>
        public static string GetUserVoiceKey()
        {
            return ConfigurationManager.AppSettings["UserVoice.Key"];
        }

        #endregion

        #region Disqus Settings

        /// <summary>
        /// Are Disqus forums enabled on packages
        /// </summary>
        /// <returns></returns>
        public static bool GetDisqusPackageEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Disqus.PackageForumEnabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Get the Disqus forum short name
        /// </summary>
        /// <returns></returns>
        public static string GetDisqusForumShortName()
        {
            return ConfigurationManager.AppSettings["Disqus.ForumShortName"];
        }

        /// <summary>
        /// Is Disqus developer mode enabled
        /// </summary>
        /// <returns></returns>
        public static bool GetDisqusDeveloperModeEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["Disqus.DeveloperModeEnabled"];
            return (enabled == "true") ? true : false;
        }

        #endregion

        #region AddThis Settings

        /// <summary>
        /// Are AddThis buttons enabled
        /// </summary>
        /// <returns></returns>
        public static bool GetAddThisEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["AddThis.Enabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Get the AddThis profile id
        /// </summary>
        /// <returns></returns>
        public static string GetAddThisProfileId()
        {
            return ConfigurationManager.AppSettings["AddThis.Profile"];
        }

        /// <summary>
        /// Is an AddThis customer widget enabled
        /// </summary>
        /// <returns></returns>
        public static bool GetAddThisCustomWidgetEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["AddThis.CustomWidgetenabled"];
            return (enabled == "true") ? true : false;
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

        #region Download Proxy Settings

        /// <summary>
        /// Is the download proxy enabled
        /// </summary>
        /// <returns></returns>
        public static bool GetDownloadProxyEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["DownloadProxy.Enabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Get the download proxy route
        /// </summary>
        /// <returns></returns>
        public static string GetDownloadProxyRoute()
        {
            string route = ConfigurationManager.AppSettings["DownloadProxy.Route"];
            return route;
        }

        /// <summary>
        /// Get the download proxy location
        /// </summary>
        /// <returns></returns>
        public static Uri GetDownloadProxyLocation(string root, out bool rootLocationFound)
        {
            string location = String.Empty;
            rootLocationFound = false;

            if (String.IsNullOrEmpty(root))
            {
                location = ConfigurationManager.AppSettings["DownloadProxy.Location"];
            }
            else
            {
                location = ConfigurationManager.AppSettings[String.Format("DownloadProxy.Location.{0}",root.ToLower())];
                rootLocationFound = true;
            }
            Uri uri = new Uri(location);
            return uri;
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