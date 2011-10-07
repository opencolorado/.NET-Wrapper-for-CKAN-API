using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CkanDotNet.Api.DataStructures;
using System.Runtime.Caching;
using CkanDotNet.Api.Model;

namespace CkanDotNet.Api.Helper
{
    internal static class Autocomplete
    {
        /// <summary>
        /// Gets a trie of package search suggestions.  If cache, the trie will be returned from the 
        /// cache. otherwise it will be created and cached.
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

            // TODO: Need to figure out how to create a unique key for the search parameters
            MemoryCache cache = MemoryCache.Default;

            CacheEntry cacheEntry = cache[key] as CacheEntry;
            if (cacheEntry == null) {
                trie = GenerateTrie(client, searchParameters, cacheSettings);

                cacheEntry = new CacheEntry();
                cacheEntry.Data = trie;
                cacheEntry.Label = "Autocomplete index";
                cacheEntry.LastCached = DateTime.Now;
                cacheEntry.Duration = cacheSettings.Duration;

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.Add(cacheSettings.Duration);

                cache.Set(key, cacheEntry, policy);
            }
            else
            {
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
            Trie trie = new Trie();

            // Get the packages
            PackageSearchResponse<Package> response = client.SearchPackages<Package>(searchParameters, cacheSettings);

            foreach (var package in response.Results)
	        {
                trie.Add(package.Title);

                foreach (var tag in package.Tags)
                {
                    trie.Add(tag.Replace("-"," "));
                }
	        }

            return trie;
        }

      
    }
}
