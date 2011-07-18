using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Collections.Specialized;
using System.Web.Mvc;
using CkanDotNet.Api.Model;

namespace CkanDotNet.Web.Models
{
    public static class TagHelper
    {
        public static Dictionary<string, int> GetTagCounts(PackageSearchResults results)
        {
            return GetTagCounts(results, new List<string>());
        }

        public static Dictionary<string, int> GetTagCounts(PackageSearchResults results, List<string> ignoreTags)
        {
            return GetTagCounts(results, ignoreTags, 0);
        }

        public static Dictionary<string, int> GetTagCounts(PackageSearchResults results, List<string> ignoreTags, int limit)
        {
            Dictionary<string, int> tagCounts = new Dictionary<string, int>();

            foreach (var result in results.Results)
            {
                foreach (var tag in result.Tags)
                {
                    if (!ignoreTags.Contains(tag))
                    {
                        if (tagCounts.ContainsKey(tag))
                        {
                            tagCounts[tag] = tagCounts[tag] + 1;
                        }
                        else
                        {
                            tagCounts.Add(tag, 1);
                        }
                    }
                }
            }

            if (limit > 0)
            {
                tagCounts = (from entry in tagCounts
                             orderby entry.Value
                             descending
                             select entry)
                             .Take(limit)
                             .ToDictionary(pair => pair.Key, pair => pair.Value);
            }

            return tagCounts;
        }
    }
}