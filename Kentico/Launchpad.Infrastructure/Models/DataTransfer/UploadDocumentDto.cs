using Azure.Search.Documents.Indexes;
using Newtonsoft.Json;


namespace Launchpad.Infrastructure.Models.DataTransfer
{

	public class UploadDocumentDto : DocumentDto
	{
		[SimpleField( IsFilterable = true, IsSortable = false ), JsonProperty( "sys_culture" )]
		public string Culture { get; set; } = "en-us";

		[SimpleField( IsFilterable = true, IsSortable = false ), JsonProperty( "sys_index" )]
		public string Index { get; set; }

		[SearchableField( IsFilterable = true ), JsonProperty( "publish_source" )]
		public string PublishSource { get; set; } = "Custom";

		[SimpleField( IsKey = true, IsSortable = true ), JsonProperty( "sys_id" )]
		public string SysId { get; set; }

		[SearchableField( IsFilterable = true, IsSortable = false ), JsonProperty( "sys_site" )]
		public string SiteName { get; set; }
	}

}