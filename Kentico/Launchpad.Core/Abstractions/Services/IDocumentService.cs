using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using System.Collections.Generic;


namespace Launchpad.Core.Abstractions.Services
{

	public interface IDocumentService : IDocumentService<PageNode>, ISearchableDocumentService<PageNode, IDocumentSpecification>
	{
		PageNode Get404Page();

		IEnumerable<PageNode> GetByType(string className, bool cacheNodes = true);

		/// <summary>
		/// Retrieves the Home node typically used as the root page of the site.
		/// </summary>
		PageNode GetHomePage();

		/// <summary>
		/// Retrieves an error page.
		/// </summary>
		PageNode GetErrorPage(int statusCode);

		PageNode Get(string path, bool useDocumentUrlPath = false);

		PageNode GetNearestAncestor(PageNode pageNode, string ancestorClassName);


		/// <summary>
		/// Retrieves the robots file from the node tree in Multisite environments.
		/// </summary>
		string GetRobotsFile( );

	}

}
