namespace Common.Migration.Import.Redirects
{
	public class ImportRedirect
	{
		public int SiteId { get; set; } = 1;
		public string MatchUrl { get; set; }
		public string RedirectUrl { get; set; }
		public ImportRedirectType ImportRedirectType { get; set; } = ImportRedirectType.Permanent;
		public bool RegexReplace { get; set; }
	}
}
