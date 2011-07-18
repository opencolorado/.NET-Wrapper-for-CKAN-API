using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Web;
using CkanDotNet.Api.Model;
using RestSharp;

namespace CkanDotNet.Api
{
    public static class Ckan
    {
        //private static string repository = "www.ckan.net";
        private static string apiVersion = "2";
        //private static string groupFilter = "climatedata";

        // TODO: Move to config
        private static string repository = "colorado.ckan.net";
        private static string groupFilter = "denver";

        /// <summary>
        /// Execute a request to the CKAN REST API.
        /// </summary>
        /// <typeparam name="T">The type that will be used for the JSPN returned.</typeparam>
        /// <param name="request">The RestRequest</param>
        /// <returns></returns>
        public static T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = String.Format("http://{0}/api/{1}/",repository,apiVersion);
            
            // Set the default date format
            if (request.DateFormat == null)
            {
                //request.DateFormat = "yyyy-MM-ddTHH:mm:ss.ffffff";
            }

            WriteRequestToConsole(client, request);
            var response = client.Execute<T>(request);
            return response.Data;
        }

        private static void WriteRequestToConsole(RestClient client, RestRequest request)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(client.BaseUrl);
            sb.Append("/");
            sb.Append(request.Resource);

            if (request.Parameters.Count > 0)
            {
                sb.Append("?");
                bool first = true;
                foreach (var parameter in request.Parameters)
                {
                    if (!first)
                    {
                        sb.Append("&");
                    }
                    else
                    {
                        first = false;
                    }

                    sb.AppendFormat("{0}={1}", parameter.Name, parameter.Value);
                }
            }

            System.Diagnostics.Debug.WriteLine(sb);
        }

        /// <summary>
        /// Get all CKAN packages ids.
        /// </summary>
        /// <returns>A list of package ids.</returns>
        public static List<string> GetPackageIds()
        {
            var request = new RestRequest();
            request.Resource = "rest/package";
            List<string> packageIds = Execute<List<string>>(request);
            return packageIds;
        }

        /// <summary>
        /// Get a CKAN package by name or by id.
        /// </summary>
        /// <param name="name">The package name or id.</param>
        /// <returns>The package</returns>
        public static Package GetPackage(string name)
        {
            var request = new RestRequest();
            request.DateFormat = "yyyy-MM-ddTHH:mm:ss.ffffff";
            request.Resource = String.Format("rest/package/{0}", name);
            Package package = Execute<Package>(request);

            return package;
        }

        /// <summary>
        /// Get all CKAN group ids.
        /// </summary>
        /// <returns>A list of group ids.</returns>
        public static List<string> GetGroupIds()
        {
            var request = new RestRequest();
            request.Resource = "rest/group";
            List<string> groupIds = Execute<List<string>>(request);
            return groupIds;
        }

        /// <summary>
        /// Get a CKAN group by name or id.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Group GetGroup(string name)
        {
            var request = new RestRequest();
            request.Resource = String.Format("rest/group/{0}", name);

            // HACK: The CKAN date format is different in the response for this request..
            request.DateFormat = "yyyy-MM-dd HH:mm:ss.ffffff";
            
            Group group = Execute<Group>(request);
            return group;
        }

        /// <summary>
        /// Get a list of package names by tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<string> GetPackageNamesByTag(string tag)
        {
            var request = new RestRequest();
            request.Resource = String.Format("rest/tag/{0}", tag);
            List<string> packageNames = Execute<List<string>>(request);
            return packageNames;
        }

        /// <summary>
        /// Get all CKAN licenses.
        /// </summary>
        /// <returns>A list of licenses.</returns>
        public static List<License> GetLicenses()
        {
            var request = new RestRequest();
            request.Resource = "rest/licenses";
            List<License> licenses = Execute<List<License>>(request);
            return licenses;
        }

        /// <summary>
        /// Get all CKAN revision ids.
        /// </summary>
        /// <returns>A list of revision ids.</returns>
        public static List<string> GetRevisionIds()
        {
            var request = new RestRequest();
            request.Resource = "rest/revision";
            List<string> revisionIds = Execute<List<string>>(request);
            return revisionIds;
        }

        /// <summary>
        /// Get all CKAN revision ids.
        /// </summary>
        /// <returns>A list of revision ids.</returns>
        public static List<string> GetRevisionIds(DateTime since)
        {
            var request = new RestRequest();
            request.Resource = "rest/revision";
            request.AddParameter("since_time", since);


            List<string> revisionIds = Execute<List<string>>(request);
            return revisionIds;
        }

        /// <summary>
        /// Get a CKAN revision by id.
        /// </summary>
        /// <returns>A list of revision ids.</returns>
        public static Revision GetRevision(string id)
        {
            var request = new RestRequest();
            request.Resource = String.Format("rest/revision/{0}", id);

            Revision revision = Execute<Revision>(request);
            return revision;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static PackageSearchResults SearchPackages(PackageSearchParameters parameters)
        {
            var request = new RestRequest();
            request.Resource = "search/package";
            request.DateFormat = "yyyy-MM-ddTHH:mm:ss.ffffff";

            // Filter by tags for now.. should be filtered by group.
            //request.AddParameter("tags", tagFilter);
            if (!String.IsNullOrEmpty(groupFilter))
            {
                request.AddParameter("groups", groupFilter);
            }

            foreach (var tag in parameters.Tags)
            {
                request.AddParameter("tags", tag);
            }
            
            request.AddParameter("all_fields", 1);

            // Add the parameters to the request
            if (!String.IsNullOrEmpty(parameters.Query))
            {
                request.AddParameter("q", parameters.Query);
            }

            if (!String.IsNullOrEmpty(parameters.Title))
            {
                request.AddParameter("title", parameters.Title);
            }

            if (!String.IsNullOrEmpty(parameters.Notes))
            {
                request.AddParameter("notes", parameters.Notes);
            }

            if (parameters.Groups.Count > 0)
            {
                //request.AddParameter("groups", parameters.Groups.To);
            }

            if (!String.IsNullOrEmpty(parameters.OrderBy))
            {
                request.AddParameter("order_by", parameters.OrderBy);
            }

            // TODO: No native support for format filtering so will have to 
            // implement this in code if needed.
            //if (!String.IsNullOrEmpty(parameters.Format))
            //{
            //    request.AddParameter("format", parameters.Format);
            //}

            // Request pagination
            //Pagination pager = new Pagination(parameters.RecordsPerPage);
            request.AddParameter("offset", parameters.Offset);
            request.AddParameter("limit", parameters.Limit);

            // Execute the request
            var results = Execute<PackageSearchResults>(request);

            if (results == null)
            {
                results = new PackageSearchResults();
            }

            return results;
        }

        /// <summary>
        /// Get a CKAN package by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //public static dynamic GetPackage(string name)
        //{
        //    string request = String.Format("rest/package/{0}", name);
        //    return CkanRequest(request);
        //}

        #region Private Methods

        /// <summary>
        /// Sends a request to the CKAN REST api.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static dynamic CkanRequest(string request)
        {
            string url = String.Format("http://{0}/api/{1}/{2}",
                repository, apiVersion, request);

            return SendCkanRequest(url);
        }

        /// <summary>
        /// Sends a request to the CKAN REST api with parameters
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static dynamic CkanRequest(string request, NameValueCollection parameters)
        {
            string url = String.Format("http://{0}/api/2/{1}{2}", 
                repository, request, BuildQueryString(parameters));

            return SendCkanRequest(url);
        }

        /// <summary>
        /// Sends a request to the CKAN REST api
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static dynamic SendCkanRequest(string url)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("Sending: {0}", url));
            string response = SendWebRequest(url);

            JsonParser parser = new JsonParser();
            return parser.Parse(response);  
        }

        /// <summary>
        /// Sends a web request.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string SendWebRequest(string url)
        {
            string response = null;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            WebResponse webResponse = webRequest.GetResponse();
            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                response = reader.ReadToEnd();
            }

            return response;
        }

        /// <summary>
        /// Build an encoded query string from a collection of name/value pairs.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private static string BuildQueryString(NameValueCollection values)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < values.Count; i++)
            {
                sb.Append(i == 0 ? "?" : "&");
                sb.Append(values.Keys[i]);
                sb.Append("=");
                sb.Append(HttpUtility.UrlEncode(values[i]));
            }
            return sb.ToString();
        }

        #endregion
    }
}
