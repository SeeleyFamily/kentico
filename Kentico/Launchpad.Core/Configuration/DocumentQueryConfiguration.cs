using Launchpad.Core.Abstractions.Configuration;


namespace Launchpad.Core.Configuration
{

	/// <summary>
	/// An implementation of <see cref="IDocumentQueryConfiguration"/>, providing properties defining request specific document query parameters.
	/// </summary>
	public class DocumentQueryConfiguration : IDocumentQueryConfiguration
	{
		public bool CheckPermissions { get; set; }
		public string Culture { get; set; }
		public bool IncludeArchived { get; set; }
		public bool IsPreview { get; set; }		
		public int SiteId { get; set; }
		public string SiteName { get; set; }

		public DocumentQueryConfiguration()
		{

		}
		
		public DocumentQueryConfiguration(IDocumentQueryConfiguration queryConfiguration)
		{
			CheckPermissions = queryConfiguration.CheckPermissions;
			Culture = queryConfiguration.Culture;
			IncludeArchived = queryConfiguration.IncludeArchived;
			IsPreview = queryConfiguration.IsPreview;
			SiteId = queryConfiguration.SiteId;
			SiteName = queryConfiguration.SiteName;
		}
	}

}
