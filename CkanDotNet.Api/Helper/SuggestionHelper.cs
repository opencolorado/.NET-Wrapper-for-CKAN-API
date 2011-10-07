using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CkanDotNet.Api.DataStructures;
using System.Runtime.Caching;
using CkanDotNet.Api.Model;

namespace CkanDotNet.Api.Helper
{
    internal static class SuggestionHelper
    {
        /// <summary>
        /// Gets a trie of package search suggestions a package search.  
        /// If cached, the trie will be returned from the 
        /// cache, otherwise it will be created and cached.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="searchParameters"></param>
        /// <param name="cacheSettings"></param>
        /// <returns></returns>
        public static Trie GetTrie(CkanClient client, PackageSearchParameters searchParameters, CacheSettings cacheSettings)
        {
            Trie trie = null;

            // Get the trie from the cache
            string key = CkanClient.CacheKeyPrefix + "AutoComplete";

            // Get the memorycache
            MemoryCache cache = MemoryCache.Default;

            // Get the cached entry if it exists
            CacheEntry cacheEntry = cache[key] as CacheEntry;
            if (cacheEntry == null) {
                // Generate the trie
                trie = GenerateTrie(client, searchParameters, cacheSettings);

                cacheEntry = new CacheEntry();
                cacheEntry.Data = trie;
                cacheEntry.Label = "Autocomplete index";
                cacheEntry.LastCached = DateTime.Now;
                cacheEntry.Duration = cacheSettings.Duration;

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.Add(cacheSettings.Duration);

                // Add the trie to the cache
                cache.Set(key, cacheEntry, policy);
            }
            else
            {
                // Get the trie from the cache
                trie = (Trie)cacheEntry.Data;
            }

            return trie;
        }

        /// <summary>
        /// Generates a trie of suggestions based on attributes of packages in the catalog
        /// </summary>
        /// <param name="client"></param>
        /// <param name="searchParameters"></param>
        /// <param name="cacheSettings"></param>
        /// <returns></returns>
        private static Trie GenerateTrie(CkanClient client, PackageSearchParameters searchParameters, CacheSettings cacheSettings)
        {
            // Create an empty trie
            Trie trie = new Trie();

            // Run the search to get all packages
            PackageSearchResponse<Package> response = client.SearchPackages<Package>(searchParameters, cacheSettings);

            // Add the entrys to the trie
            foreach (var package in response.Results)
	        {
                // Add the package title
                trie.Add(package.Title);

                // Add the package tags (removing hyphens which represent spaces)
                foreach (var tag in package.Tags)
                {
                    trie.Add(tag.Replace("-"," "));
                }
	        }

            return trie;
        }
    }
}
