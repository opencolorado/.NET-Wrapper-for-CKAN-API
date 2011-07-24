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
    public class WhenUsingCkanSearchResourceApi
    {
        public WhenUsingCkanSearchResourceApi()
        {
            LogHelper.ConfigureLoggingToTrace();
        }

        [Fact]
        public void ShouldReturnResources()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            var parameters = new ResourceSearchParameters();

            var response = client.SearchResources<Resource>(parameters);

            Assert.NotEmpty(response.Results);
        }

        [Fact]
        public void ShouldReturnResourcesIds()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            var parameters = new ResourceSearchParameters();

            var response = client.SearchResources<string>(parameters);

            Assert.NotEmpty(response.Results);
        }

        [Fact]
        public void ShouldReturnResourcesByFormat()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            var parameters = new ResourceSearchParameters();
            parameters.Format = "html";

            var response = client.SearchResources<Resource>(parameters);

            Assert.NotEmpty(response.Results);

            foreach (var result in response.Results)
            {
                Assert.Contains("html",result.Format,StringComparison.InvariantCultureIgnoreCase);
            }
        }

        [Fact]
        public void ShouldReturnResourcesByDescription()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            var parameters = new ResourceSearchParameters();
            parameters.Description = "university";

            var response = client.SearchResources<Resource>(parameters);

            Assert.NotEmpty(response.Results);

            Assert.Contains("university", response.Results[0].Description ,StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public void ShouldReturnResourcesWithLimitAndOffset()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            var parameters = new ResourceSearchParameters();
            parameters.Limit = 1;
            parameters.Offset = 1;

            var response = client.SearchResources<Resource>(parameters);

            Assert.True(response.Results.Count == 1);
         }
    }
}
