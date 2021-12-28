using System.Collections.Generic;

namespace Launchpad.Core.Constants
{

	public static class Constants
	{
		public static string DocumentCustomDataSearchBlobKey = "SearchBlob";
		public static string DocumentForeignKeyValueColumnName = "DocumentForeignKeyValue";
		public static string DocumentCustomDataForceNoUpdateKey = "ForceNoUpdate";
		public static string DocumentUrlPath = "DocumentUrlPath";
		public static string DocumentUrlPathUpdateChildren = "DocumentUrlPathUpdateChildren";
		public static readonly List<string> NonIndexablePaths = new List<string>() {
			"/example-content/",
			"/migrated-content/",
			"/content-migration/",
			"/cms/",			
		};
	}

}
