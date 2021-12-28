using Custom.Core.Models;
using Launchpad.Core.Abstractions.Services;

namespace Custom.Infrastructure.Abstractions.Services
{
	public interface ISiteSettingsService : IGlobalContentDocumentService<SiteSettingsModel>, IBaseSiteSettingsService
	{
	}
}
