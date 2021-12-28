using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using System;
using System.Web;


namespace Launchpad.Infrastructure.Providers
{

	public class CurrentNodeProvider : ICurrentNodeProvider
	{
		#region Fields
		private readonly Lazy<IDocumentService> documentService;
		#endregion

		#region Properties
		protected virtual string CurrentUrl { get; set; }
		protected virtual bool IsNotFound { get; set; }
		protected virtual PageNode Node { get; set; }
		#endregion


		public CurrentNodeProvider
		(
			Lazy<IDocumentService> documentService,
			HttpContextBase httpContext
		)
		{
			this.documentService = documentService;

			CurrentUrl = httpContext.Request.CurrentExecutionFilePath.ToLower();
		}


		public PageNode GetCurrentNode( )
		{
			if( Node == null && !IsNotFound )
			{
				SetCurrentNodeInternal();
			}

			return Node;
		}


		public void SetCurrentNode( PageNode node )
		{
			Node = node;
			IsNotFound = ( node == null );

			if( node != null )
			{
				CurrentUrl = Node.NodeAliasPath.ToLower();
			}
		}


		public void SetCurrentNode( string nodeAliasPath )
		{
			CurrentUrl = nodeAliasPath;
			IsNotFound = false;
			Node = null;

			SetCurrentNodeInternal();
		}


		protected virtual void SetCurrentNodeInternal( )
		{
			// Set to the current path's node
			Node = documentService.Value.Get(CurrentUrl, useDocumentUrlPath: true);

			if ( Node == null )
			{
				IsNotFound = true;
			}
		}
	}
}