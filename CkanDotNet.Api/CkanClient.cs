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
using log4net;
using System.Reflection;

namespace CkanDotNet.Api
{
    /// <summary>
    /// A client for the CKAN API v2.
    /// </summary>
    public class CkanClient
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The supported CKAN api version
        /// </summary>
        private const string apiVersion = "2";

        /// <summary>
        /// Gets or sets the CKAN repository host name
        /// </summary>
        private string Repository { get; set; }

        /// <summary>
        /// Create an instance of the Ckan client.
        /// </summary>
        /// <param name="repository">The hostname of the CKAN repository.</param>
        public CkanClient(string repository)
        {
            this.Repository = repository;
        }

        #region Public Methods

        /// <summary>
        /// Execute a request for the CKAN REST API.
        /// </summary>
        /// <typeparam name="T">The type that will be used for the JSON returned.</typeparam>
        /// <param name="request">The RestRequest</param>
        /// <returns></returns>
        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = String.Format("http://{0}/api/{1}/",this.Repository,apiVersion);
            
            // Log the request that is being sent to CKAN
            log.InfoFormat("Sending request to CKAN repository: {0}", GetRequestUrl(client, request));

            var response = client.Execute<T>(request);

            log.DebugFormat("Response recieved: {0}", response.Content);

            return response.Data;
        }

        /// <summary>
        /// Get all CKAN packages ids.
        /// </summary>
        /// <returns>A list of package ids.</returns>
        public List<string> GetPackageIds()
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
        public Package GetPackage(string name)
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
        public List<string> GetGroupIds()
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
        public Group GetGroup(string name)
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
        public List<string> GetPackageNamesByTag(string tag)
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
        public List<License> GetLicenses()
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
        public List<string> GetRevisionIds()
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
        public List<string> GetRevisionIds(DateTime since)
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
        public Revision GetRevision(string id)
        {
            var request = new RestRequest();
            request.Resource = String.Format("rest/revision/{0}", id);

            Revision revision = Execute<Revision>(request);
            return revision;
        }

        /// <summary>
        /// Search the CKAN repository.
        /// </summary>
        /// <param name="parameters">Provides that parameters to use in the search.</param>
        /// <returns>Search results</returns>
        public PackageSearchResults SearchPackages(PackageSearchParameters parameters)
        {
            var request = new RestRequest();
            request.Resource = "search/package";
            request.DateFormat = "yyyy-MM-ddTHH:mm:ss.ffffff";

            // Apply group parameters
            foreach (var group in parameters.Groups)
            {
                request.AddParameter("groups", group);
            }

            // Apply tag parameters
            foreach (var tag in parameters.Tags)
            {
                request.AddParameter("tags", tag);
            }
            
            // Apply all_fields parameter
            request.AddParameter("all_fields", 1);

            // Apply query parameter
            if (!String.IsNullOrEmpty(parameters.Query))
            {
                request.AddParameter("q", parameters.Query);
            }

            // Apply title parameter
            if (!String.IsNullOrEmpty(parameters.Title))
            {
                request.AddParameter("title", parameters.Title);
            }

            // Apply notes parameter
            if (!String.IsNullOrEmpty(parameters.Notes))
            {
                request.AddParameter("notes", parameters.Notes);
            }

            // Apply order_by parameter
            if (!String.IsNullOrEmpty(parameters.OrderBy))
            {
                request.AddParameter("order_by", parameters.OrderBy);
            }

            // Set the offset and limit (pagination)
            request.AddParameter("offset", parameters.Offset);
            request.AddParameter("limit", parameters.Limit);

            // Execute the request
            var results = Execute<PackageSearchResults>(request);

            // If no results, return an empty results object
            if (results == null)
            {
                results = new PackageSearchResults();
            }

            return results;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the URL represtation of the request that will be submitted to CKAN
        /// for diagnostic purposes.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        private string GetRequestUrl(RestClient client, RestRequest request)
        {
            StringBuilder sb = new StringBuilder();

            // Append the base url
            sb.Append(client.BaseUrl);
            sb.Append("/");
            sb.Append(request.Resource);

            // Append the querystring parameters
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

            return sb.ToString();
        }

        #endregion
    }
}
