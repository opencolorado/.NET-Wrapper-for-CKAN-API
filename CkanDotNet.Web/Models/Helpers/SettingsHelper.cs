using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CkanDotNet.Api.Model;
using System.Configuration;

namespace CkanDotNet.Web.Models.Helpers
{
    public static class SettingsHelper
    {
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
    }
}