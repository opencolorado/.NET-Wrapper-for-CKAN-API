using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using log4net.Repository.Hierarchy;
using log4net;
using log4net.Appender;
using log4net.Layout;
using CkanDotNet.Api.Tests.Helpers;
using CkanDotNet.Api.Model;

namespace CkanDotNet.Api.Tests
{
    public class WhenUsingCkanSearchPackageApi
    {
        public WhenUsingCkanSearchPackageApi()
        {
            LogHelper.ConfigureLoggingToTrace();
        }

        [Fact]
        public void ShouldReturnPackagesWithQuery()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            PackageSearchParameters parameters = new PackageSearchParameters();
            parameters.Query = "road";

            PackageSearchResponse<Package> response = client.SearchPackages<Package>(parameters);

            Assert.NotEmpty(response.Results);
        }

        [Fact]
        public void ShouldReturnSinglePackagesWithQueryLimit()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            PackageSearchParameters parameters = new PackageSearchParameters();
            parameters.Query = "road";
            parameters.Offset = 0;
            parameters.Limit = 1;

            PackageSearchResponse<Package> response = client.SearchPackages<Package>(parameters);

            Assert.True(response.Results.Count == 1);
        }

        [Fact]
        public void ShouldReturnPackageIdsWithQuery()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            PackageSearchParameters parameters = new PackageSearchParameters();
            parameters.Query = "road";

            PackageSearchResponse<string> response = client.SearchPackages<string>(parameters);

            Assert.NotEmpty(response.Results);
        }

        [Fact]
        public void ShouldReturnPackagesWithGroup()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            PackageSearchParameters parameters = new PackageSearchParameters();
            parameters.Groups.Add("art");

            PackageSearchResponse<Package> response = client.SearchPackages<Package>(parameters);

            Assert.NotEmpty(response.Results);

            foreach (var result in response.Results)
            {
                Assert.Contains<string>("art", result.Groups);
            }
        }

        [Fact]
        public void ShouldReturnPackagesWithTag()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            PackageSearchParameters parameters = new PackageSearchParameters();
            parameters.Tags.Add("academic");

            PackageSearchResponse<Package> response = client.SearchPackages<Package>(parameters);

            Assert.NotEmpty(response.Results);
        }
    }
}
