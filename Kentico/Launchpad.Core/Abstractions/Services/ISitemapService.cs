using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using System.Collections.Generic;
using System.Xml.Linq;


namespace Launchpad.Core.Abstractions.Services
{
	public interface ISitemapService
	{		
		XDocument GetSitemap();		
	}

}
