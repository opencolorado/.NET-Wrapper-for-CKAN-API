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
            CkanClient client = CkanApiHelper.GetCkanClient();

            var parameters = new ResourceSearchParameters();
            parameters.Description = "city";

            var response = client.SearchResources<Resource>(parameters);

            Assert.NotEmpty(response.Results);
        }

        [Fact]
        public void ShouldReturnResourcesIds()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            var parameters = new ResourceSearchParameters();
            parameters.Description = "city";

            var response = client.SearchResources<string>(parameters);

            Assert.NotEmpty(response.Results);
        }

        [Fact]
        public void ShouldReturnResourcesByFormat()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            var parameters = new ResourceSearchParameters();
            parameters.Format = "kml";

            var response = client.SearchResources<Resource>(parameters);

            Assert.NotEmpty(response.Results);

            foreach (var result in response.Results)
            {
                Assert.Contains("kml",result.Format,StringComparison.InvariantCultureIgnoreCase);
            }
        }

        [Fact]
        public void ShouldReturnResourcesByDescription()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            var parameters = new ResourceSearchParameters();
            parameters.Description = "city";

            var response = client.SearchResources<Resource>(parameters);

            Assert.NotEmpty(response.Results);

            Assert.Contains("city", response.Results[0].Description, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public void ShouldReturnResourcesWithLimitAndOffset()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            var parameters = new ResourceSearchParameters();
            parameters.Limit = 1;
            parameters.Offset = 1;
            parameters.Description = "city";

            var response = client.SearchResources<Resource>(parameters);

            Assert.True(response.Results.Count == 1);
         }
    }
}
