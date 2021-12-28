using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Services;
using System.Collections.Generic;

namespace Launchpad.Infrastructure.Services
{
	public abstract class GlobalContentDocumentService<TPageType, T> : DocumentService<TPageType, T>
		where TPageType : TreeNode, new()
		where T : class
	{
		#region Fields
		protected new readonly IGlobalContentDocumentService<TPageType> documentService;
		#endregion

		public GlobalContentDocumentService(IGlobalContentDocumentService<TPageType> documentService) : base(documentService)
		{
			this.documentService = documentService;
		}

		public virtual IEnumerable<T> GetFromGlobalContent()
		{
			return Convert(documentService.GetFromGlobalContent());
		}
	}
}
