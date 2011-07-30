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
        /// Get the list of groups that have been configured in the settings.
        /// </summary>
        /// <returns></returns>
        public static bool GetPackageRssFeedEnabled()
        {
            string enabled = ConfigurationManager.AppSettings["PackageRSSFeedEnabled"];
            return (enabled == "true") ? true : false;
        }

        /// <summary>
        /// Get the list of groups that have been configured in the settings.
        /// </summary>
        /// <returns></returns>
        public static int GetPackageRssFeedDays()
        {
            string days = ConfigurationManager.AppSettings["PackageRSSFeedDays"];
            return int.Parse(days);
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
        public static void FilterTags(List<string> tags)
        {
            List<string> hiddenTags = GetHiddenTags();
            foreach (var hiddenTag in hiddenTags)
            {
                tags.Remove(hiddenTag);
            }
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