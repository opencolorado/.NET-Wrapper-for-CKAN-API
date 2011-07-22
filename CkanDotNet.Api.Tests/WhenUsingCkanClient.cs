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
        public void ShouldReturnGroupByIdAndName()
        {
            CkanClient client = new CkanClient("test.ckan.net");
            
            List<string> groups = client.GetGroupIds();

            Group group = client.GetGroup(groups[0]);

            Assert.NotNull(group);

            group = client.GetGroup(group.Name);

            Assert.NotNull(group);
        }

        [Fact]
        public void ShouldReturnLicenses()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            List<License> licenses = client.GetLicenses();

            Console.WriteLine("Licenses found: {0}", licenses.Count);

            Assert.NotEmpty(licenses);
        }

        [Fact]
        public void ShouldReturnPackageIds()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            List<string> packageIds = client.GetPackageIds();

            Console.WriteLine("Package ids found: {0}", packageIds.Count);

            Assert.NotEmpty(packageIds);
        }

        [Fact]
        public void ShouldReturnPackageByIdAndName()
        {
            CkanClient client = new CkanClient("test.ckan.net");

            List<string> packageIds = client.GetPackageIds();

            Package package = client.GetPackage(packageIds[0]);

            Assert.NotNull(package);

            package = client.GetPackage(package.Name);

            Assert.NotNull(package);
        }
    }
}
