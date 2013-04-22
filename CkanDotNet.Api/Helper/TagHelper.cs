using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CkanDotNet.Api.Model;

namespace CkanDotNet.Api.Helper
{
    public static class TagHelper
    {
        /// <summary>
        /// Get top tags from a collection of packages ignoring specific tags.
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="ignoreTags"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<Tag> GetTagCounts(List<Package> packages)
        {
            List<Tag> tags = new List<Tag>();

            foreach (var package in packages)
            {
                foreach (var tagString in package.Tags)
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

            return tags;
        }
    }
}
