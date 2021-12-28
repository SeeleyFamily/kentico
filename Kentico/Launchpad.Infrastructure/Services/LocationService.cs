using System;
using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Extensions;


namespace Launchpad.Infrastructure.Services
{

	public class LocationService<T, TPageType, TSpecification> : DocumentService<TPageType, T>, ILocationService<T, TSpecification>, IPerScopeService
		where T : Location, new()
		where TPageType : TreeNode, new()
		where TSpecification : ILocationSpecification
	{
		#region Fields
		protected readonly IDocumentQueryConfiguration queryConfiguration;
		#endregion


		public LocationService
		(
			IDocumentQueryConfiguration queryConfiguration,

			IDocumentService<TPageType> documentService
		) 
			: base( documentService )
		{
			this.queryConfiguration = queryConfiguration;
		}



		public virtual PagedResult<T> Find( TSpecification specification )
		{
			DocumentQuery<TPageType> query = DocumentHelper.GetDocuments<TPageType>()
														   .ApplyConfiguration( queryConfiguration )
														   .ApplyPagingSpecification( specification )
														   .ApplyLocationSpecification( specification );


			return query.ToPagedResult( specification )
						.ConvertTo( Convert );						
		}



		protected override T Convert( TPageType node )
		{
			return new T
			{
				Address = node.GetAddress(),
				EmailAddress = node.GetStringValue( "EmailAddress", String.Empty ),
				Id = node.NodeID,
				Name = node.DocumentName,
				PhoneNumber = node.GetStringValue( "PhoneNumber", String.Empty ),

			};
		}
	}

}
