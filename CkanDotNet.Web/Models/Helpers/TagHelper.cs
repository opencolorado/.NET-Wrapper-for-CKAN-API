using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Collections.Specialized;
using System.Web.Mvc;
using CkanDotNet.Api.Model;
using CkanDotNet.Web.Models.Helpers;

namespace CkanDotNet.Web.Models
{
    public static class TagHelper
    {
        /// <summary>
        /// Get tag counts from a collection of packages.
        /// </summary>
        /// <param name="packages"></param>
        /// <returns></returns>
        public static List<Tag> GetTagCounts(Package package)
        {
            var packages = new List<Package>();
            packages.Add(package);
            return GetTagCounts(packages, 0, SettingsHelper.GetTagCloudMinScale(), SettingsHelper.GetTagCloudMaxScale());
        }

        /// <summary>
        /// Get tag counts from a collection of packages.
        /// </summary>
        /// <param name="packages"></param>
        /// <returns></returns>
        public static List<Tag> GetTagCounts(List<Package> packages)
        {
            return GetTagCounts(packages, 0, SettingsHelper.GetTagCloudMinScale(), SettingsHelper.GetTagCloudMaxScale());
        }

        /// <summary>
        /// Get tag counts from a collection of packages
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="ignoreTags"></param>
        /// <returns></returns>
        public static List<Tag> GetTagCounts(List<Package> packages, int minPercent, int maxPercent)
        {
            return GetTagCounts(packages, new List<string>(), 0, minPercent, maxPercent);
        }

        /// <summary>
        /// Get tag counts from a collection of packages ignoring specific tags
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="ignoreTags"></param>
        /// <returns></returns>
        public static List<Tag> GetTagCounts(List<Package> packages, int limit, int minPercent, int maxPercent)
        {
            return GetTagCounts(packages, new List<string>(), 0, minPercent, maxPercent);
        }

        /// <summary>
        /// Get top tags from a collection of packages ignoring specific tags.
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="ignoreTags"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<Tag> GetTagCounts(List<Package> packages, List<string> ignoreTags, int limit, int minPercent, int maxPercent)
        {
            List<Tag> tags = new List<Tag>();

            foreach (var package in packages)
            {
                foreach (var tagString in package.Tags)
                {
                    if (!ignoreTags.Contains(tagString))
                    {
                        Tag tag = new Tag(tagString);

                        if (tags.Contains(tag))
                        {
                            tag = tags.Find(item => item.Label == tagString);
                            tag.Count = tag.Count + 1;
                        }
                        else
                        {
                            tags.Add(tag);
                        }
                    }
                }  
            }

            if (tags.Count > 0)
            {
                // Get the top items from the list
                if (limit > 0)
                {
                    tags = (from entry in tags
                            orderby entry.Count
                            descending
                            select entry)
                                 .Take(limit)
                                 .ToList();
                }

                // Order the list
                tags.Sort();

                // Compute the tag scales
                tags = ComputeTagScale(tags, minPercent, maxPercent);
            }

            return tags;
        }

        /// <summary>
        /// Compute the relative size of each tag based on the total number of tags
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="minPercent"></param>
        /// <param name="maxPercent"></param>
        /// <returns></returns>
        private static List<Tag> ComputeTagScale(List<Tag> tags, int minPercent, int maxPercent)
        {
            int min = (from entry in tags
                       orderby entry.Count
                       ascending
                       select entry).First().Count;

            int max = (from entry in tags
                       orderby entry.Count
                       ascending
                       select entry).Last().Count;

            int count = tags.Count;

            var multiplier = 1;

            if (max > min && maxPercent > minPercent)
            {
                multiplier = (maxPercent - minPercent) / (max - min);
            }

            foreach (var tag in tags)
            {
                double size = minPercent + ((max - (max - (tag.Count - min))) * multiplier);
                tag.Scale = (int)Math.Round(size);
            }

            return tags;
        }
    }
}