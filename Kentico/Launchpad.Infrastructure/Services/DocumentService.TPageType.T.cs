using System;
using System.Collections.Generic;
using System.Linq;
using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;


namespace Launchpad.Infrastructure.Services
{

	/// <summary>
	/// Provides an abstract class to query specific tree nodes of type <typeparamref name="TPageType"/> and convert them to
	/// <typeparamref name="T"/>.
	/// Use this class as a base for another class when you want to query specific page types but convert them to another model
	/// first.
	/// </summary>
	public abstract class DocumentService<TPageType, T> : IDocumentService<T>
		where TPageType : TreeNode, new()
		where T : class
	{
		#region Fields
		protected readonly IDocumentService<TPageType> documentService;
		#endregion


		public DocumentService
		(
			IDocumentService<TPageType> documentService
		)
		{
			this.documentService = documentService;
		}



		//public virtual PagedResult<T> Find( IDocumentSpecification specification )
		//{
		//	return Convert( documentService.Find( specification ) );
		//}


		public virtual IEnumerable<T> Get( ICacheConfiguration cacheConfiguration = null )
		{
			return Convert( documentService.Get( cacheConfiguration ) );
		}


		public virtual IEnumerable<T> Get( string[] columns )
		{
			return Convert(documentService.Get(columns));
		}


		public virtual T Get( int id )
		{
			var node = documentService.Get(id);
			if(node == null)
			{
				return null;
			}
			return Convert(node);
		}


		public virtual T Get( string path )
		{
			var node = documentService.Get(path);
			if (node == null)
			{
				return null;
			}
			return Convert(node);
		}


        public virtual T Get(Guid guid)
        {
			var node = documentService.Get(guid);
			if (node == null)
			{
				return null;
			}
			return Convert(node);
        }


		public virtual IEnumerable<T> Get(IEnumerable<Guid> guids)
		{
			return Convert(documentService.Get(guids));
		}


		public virtual IEnumerable<T> GetByParent( int id, int count = 0 )
		{
			return Convert( documentService.GetByParent( id, count ) );
		}


		public virtual IEnumerable<T> GetByParent( string path, int count = 0 )
		{
			return Convert( documentService.GetByParent( path, count ) );
		}


        public IEnumerable<T> GetByParent(Guid guid, int count = 0)
        {
			return Convert(documentService.GetByParent(guid, count));
		}


		public IEnumerable<T> GetFromFolder<TFolderType>( string path, int count = 0 )
			where TFolderType : class
		{
			return Convert( documentService.GetFromFolder<TFolderType>( path, count ) );
		}


		public IEnumerable<T> GetFromGlobalContent(int count = 0)
		{
			return Convert(documentService.GetFromGlobalContent(count));
		}


		public bool IsNodeAuthorizedForUser( PageNode node, IUser user )
		{
			return documentService.IsNodeAuthorizedForUser( node, user );
		}


		protected abstract T Convert( TPageType node );


		protected virtual IEnumerable<T> Convert( IEnumerable<TPageType> items )
		{
			return items.Where(n=> n != null ).Select( n => Convert( n ) ).ToArray();
		}


		protected virtual PagedResult<T> Convert( PagedResult<TPageType> result )
		{
			return new PagedResult<T>
			{
				Items = Convert( result.Items ),
				PageIndex = result.PageIndex,
				PageSize = result.PageSize,
				RowEnd = result.RowEnd,
				RowStart = result.RowStart,
				Specification = result.Specification,
				Total = result.Total,
				TotalPages = result.TotalPages
			};
		}

	}

}
