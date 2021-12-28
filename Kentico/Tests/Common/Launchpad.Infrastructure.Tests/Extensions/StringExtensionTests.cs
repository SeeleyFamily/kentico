using CMS.Tests;
using Launchpad.Core.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Infrastructure.Tests.Extensions
{
	public class StringExtensionTests : IntegrationTests
	{
		[Test]
		public void GetParentPath()
		{
			var path = "";
			var parentPath = "";
			Assert.AreEqual(path.GetParentPath(), parentPath);

			path = "/";
			parentPath = "";
			Assert.AreEqual(path.GetParentPath(), parentPath);

			path = "/examples";
			parentPath = "/";
			Assert.AreEqual(path.GetParentPath(), parentPath);

			path = "/examples/Example-Overview/Example-Listing/example-detail-1";
			parentPath = "/examples/Example-Overview/Example-Listing";
			Assert.AreEqual(path.GetParentPath(), parentPath);
		}
	}
}
