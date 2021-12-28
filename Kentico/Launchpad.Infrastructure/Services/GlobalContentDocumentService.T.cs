
using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.Abstractions.Services;
using System.Collections.Generic;

namespace Launchpad.Infrastructure.Services
{
    public class GlobalContentDocumentService<T> : DocumentService<T>, IGlobalContentDocumentService<T>, IPerScopeService
		where T : TreeNode, new()
	{
		public GlobalContentDocumentService
		(
			IDocumentQueryConfiguration queryConfiguration,
			ICacheService cacheService
		) : base(queryConfiguration, cacheService)
		{
		}

		public virtual IEnumerable<T> GetFromGlobalContent()
		{
			return base.GetFromGlobalContent();
		}
	}
}
