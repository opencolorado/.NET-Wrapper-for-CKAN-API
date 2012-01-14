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
    public class WhenUsingCkanModelApi
    {
        public WhenUsingCkanModelApi()
        {
            LogHelper.ConfigureLoggingToTrace();
        }

        [Fact]
        public void ShouldReturnGroupIds()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            List<string> groups = client.GetGroupIds();

            Console.WriteLine("Groups found: {0}", groups.Count);

            Assert.NotEmpty(groups);
        }

        [Fact]
        public void ShouldReturnGroupByIdAndName()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();
            
            List<string> groups = client.GetGroupIds();

            Group group = client.GetGroup(groups[0]);

            Assert.NotNull(group);

            group = client.GetGroup(group.Name);

            Assert.NotNull(group);
        }

        [Fact]
        public void ShouldReturnPackageIds()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            List<string> packageIds = client.GetPackageIds();

            Console.WriteLine("Package ids found: {0}", packageIds.Count);

            Assert.NotEmpty(packageIds);
        }

        [Fact]
        public void ShouldReturnPackageByIdAndName()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            List<string> packageIds = client.GetPackageIds();

            Package package = client.GetPackage(packageIds[0]);

            Assert.NotNull(package);

            package = client.GetPackage(package.Name);

            Assert.NotNull(package);
        }

        [Fact]
        public void ShouldReturnTags()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            List<string> tags = client.GetTags();

            Console.WriteLine("Groups found: {0}", tags.Count);

            Assert.NotEmpty(tags);
        }

        /// <summary>
        /// TODO: This test no longer runs and cannot return the package revisions.  It isn't clear what
        /// has changed in the package API but package resources are no longer being returned.
        /// </summary>
        //[Fact]
        //public void ShouldReturnPackageRevisions()
        //{
        //    CkanClient client = CkanApiHelper.GetCkanClient();

        //    List<string> packageIds = client.GetPackageIds();

        //    Package package = client.GetPackage(packageIds[0]);

        //    List<Revision> revisions = client.GetPackageRevisions(package.Id);

        //    Console.WriteLine("Revisions found: {0}", revisions.Count);

        //    Assert.NotEmpty(revisions);
        //}

        [Fact]
        public void ShouldReturnRevisionIds()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            List<string> revisionIds = client.GetRevisionIds();

            Console.WriteLine("Revision ids found: {0}", revisionIds.Count);

            Assert.NotEmpty(revisionIds);
        }

        [Fact]
        public void ShouldReturnRevisionById()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            List<string> revisionIds = client.GetRevisionIds();

            Revision revision = client.GetRevision(revisionIds[0]);

            Assert.NotNull(revision);
        }

        [Fact]
        public void ShouldReturnLicenses()
        {
            CkanClient client = CkanApiHelper.GetCkanClient();

            List<License> licenses = client.GetLicenses();

            Console.WriteLine("Licenses found: {0}", licenses.Count);

            Assert.NotEmpty(licenses);
        }
    }
}
