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
    public class WhenUsingCkanClient
    {
        public WhenUsingCkanClient()
        {
            LogHelper.ConfigureLoggingToTrace();
        }

        [Fact]
        public void ShouldReturnGroupIds()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            List<string> groups = client.GetGroupIds();

            Console.WriteLine("Groups found: {0}", groups.Count);

            Assert.NotEmpty(groups);
        }

        [Fact]
        public void ShouldReturnGroupById()
        {
            CkanClient client = new CkanClient("test.ckan.net");
            
            List<string> groups = client.GetGroupIds();

            Group group = client.GetGroup(groups[0]);

            Assert.True(group.Name.Length > 0);
        }
    }
}
