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
            CkanClient client = CkanApiHelper.GetCkanClient();

            PackageSearchParameters parameters = new PackageSearchParameters();
            parameters.Query = "road";

            PackageSearchResponse<Package> response = client.SearchPackages<Package>(parameters);

            Assert.NotEmpty(response.Results);
        }

        [Fact]
        public void ShouldReturnSinglePackagesWithQueryLimit()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            PackageSearchParameters parameters = new PackageSearchParameters();
            parameters.Query = "bike";
            parameters.Offset = 0;
            parameters.Limit = 1;

            PackageSearchResponse<Package> response = client.SearchPackages<Package>(parameters);

            Assert.True(response.Results.Count == 1);
        }

        [Fact]
        public void ShouldReturnPackageIdsWithQuery()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            PackageSearchParameters parameters = new PackageSearchParameters();
            parameters.Query = "bike";

            PackageSearchResponse<string> response = client.SearchPackages<string>(parameters);

            Assert.NotEmpty(response.Results);
        }

        [Fact]
        public void ShouldReturnPackagesWithGroup()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            PackageSearchParameters parameters = new PackageSearchParameters();
            parameters.Groups.Add("arvada");

            PackageSearchResponse<Package> response = client.SearchPackages<Package>(parameters);

            Assert.NotEmpty(response.Results);

            foreach (var result in response.Results)
            {
                Assert.Contains<string>("arvada", result.Groups);
            }
        }

        [Fact]
        public void ShouldReturnPackagesWithTag()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            PackageSearchParameters parameters = new PackageSearchParameters();
            parameters.Tags.Add("colorado");

            PackageSearchResponse<Package> response = client.SearchPackages<Package>(parameters);

            Assert.NotEmpty(response.Results);
        }
    }
}
