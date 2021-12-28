


namespace Launchpad.Core.Abstractions.Configuration
{

	/// <summary>
	/// An interface defining properties for document queries.
	/// </summary>
	public interface IDocumentQueryConfiguration
	{
		bool CheckPermissions { get; set; }
		string Culture { get; set; }
		bool IncludeArchived { get; set; }
		bool IsPreview { get; set; }				
		int SiteId { get; set; }
		string SiteName { get; set; }		
	}

}
