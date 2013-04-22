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
        /// Compute the relative size of each tag based on the total number of tags
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="minPercent"></param>
        /// <param name="maxPercent"></param>
        /// <returns></returns>
        public static List<Tag> ComputeTagScale(List<Tag> tags)
        {
            if (tags.Count > 0)
            {
                int minPercent = SettingsHelper.GetTagCloudMinScale();
                int maxPercent = SettingsHelper.GetTagCloudMaxScale();

                double min = (from entry in tags
                              orderby entry.Count
                              ascending
                              select entry).First().Count;

                double max = (from entry in tags
                              orderby entry.Count
                              ascending
                              select entry).Last().Count;

                int count = tags.Count;

                double multiplier = 1;

                if (max > min && maxPercent > minPercent)
                {
                    multiplier = (double)(maxPercent - minPercent) / (double)(max - min);
                }

                foreach (var tag in tags)
                {
                    double size = (double)minPercent + ((max - (max - ((double)tag.Count - min))) * multiplier);
                    tag.Scale = (int)Math.Round(size);
                }
            }

            return tags;
        }


        /// <summary>
        /// Remove tags that should be hidden
        /// </summary>
        /// <param name="packages"></param>
        public static List<string> FilterTags(List<string> tags)
        {
            List<string> hiddenTags = SettingsHelper.GetCatalogHiddenTags();
            foreach (var hiddenTag in hiddenTags)
            {
                tags.Remove(hiddenTag);
            }
            return tags;
        }

        /// <summary>
        /// Remove tags that should be hidden
        /// </summary>
        /// <param name="packages"></param>
        public static List<Tag> FilterTags(List<Tag> tags)
        {
            List<string> hiddenTags = SettingsHelper.GetCatalogHiddenTags();
            List<Tag> filteredTags = new List<Tag>();

            foreach (var tag in tags)
            {
                if (!hiddenTags.Contains(tag.Label)) {
                    filteredTags.Add(tag);
                }
            }
            return filteredTags;
        }

        /// <summary>
        /// Limit tags to the top n tags with the largest count
        /// </summary>
        /// <param name="packages"></param>
        public static List<Tag> LimitTags(List<Tag> tags, int limit)
        {
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
            }

            return tags;
        }
    }
}