using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using RestSharp;
using System.Security.Cryptography;

namespace CkanDotNet.Api.Helper
{
    public static class CacheHelper
    {
        /// <summary>
        /// Get a RestResponse from the cached based on the URL.
        /// </summary>
        /// <typeparam name="T">The RestResponse type to retrieve from the cache.</typeparam>
        /// <param name="url">The url</param>
        /// <returns></returns>
        public static RestResponse<T> Get<T>(string url)
        {
            string key = Checksum(url); 
            
            MemoryCache cache = MemoryCache.Default;
            
            return cache[key] as RestResponse<T>;
        }

        /// <summary>
        /// Insert a RestResponse into the cache based on the URL.
        /// </summary>
        /// <typeparam name="T">The RestResponse type to cache.</typeparam>
        /// <param name="url">The url</param>
        /// <param name="response">The RestResponse to cache</param>
        /// <param name="settings">The cache settings</param>
        /// <returns></returns>
        public static RestResponse<T> Insert<T>(string url, RestResponse<T> response, CacheSettings settings)
        {
            string key = Checksum(url);

            MemoryCache cache = MemoryCache.Default;

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.Add(settings.Duration);
            cache.Set(key, response, policy);

            return cache[key] as RestResponse<T>;
        }

        /// <summary>
        /// Compute a checksum of the URL to use as a key
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Checksum(string url)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(url);
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(bytes)).Replace("-","").ToLower();
        }
    }
}
