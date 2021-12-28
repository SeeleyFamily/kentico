using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Tests;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Enums;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Models.DataTransfer;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class AzureSearchServiceTests : IntegrationTests
	{
		#region Fields
		private readonly IAzureSearchService service;
		#endregion


		public AzureSearchServiceTests( )
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateAzureSearchService();
		}



		[Test]
		public void ConvertsWithCustomMapper( )
		{
			// Arrange
			ISearchIndexSpecification specification = ServiceCreatorUtility.CreateSearchIndexSpecification();
			specification.IndexName = "cxlp-kentico-sqki-lp-global-dev";
			specification.Query = "example";

			SummaryItem mapper( DocumentDto dto )
			{
				return new SummaryItem
				{
					Id = dto.Id,
					Title = dto.Title,
					Url = "Custom Mapped"
				};
			}


			// Act
			PagedResult<SummaryItem> results = service.Search<SummaryItem, DocumentDto>( specification, mapper );



			// Assert
			Assert.IsNotNull( results );
			Assert.IsNotEmpty( results.Items );
			Assert.IsTrue( results.Items.All( i => !String.IsNullOrWhiteSpace( i.Id ) ), "Some documents had no ID." );
			Assert.IsTrue( results.Items.All( i => !String.IsNullOrWhiteSpace( i.Title ) ), "Some documents had no title." );
			Assert.IsTrue( results.Items.All( i => i.Url == "Custom Mapped" ), "Some documents did not custom map their URL." );
		}


		[Test]
		public void FiltersResults( )
		{
			// Arrange
			ISearchIndexSpecification specification = ServiceCreatorUtility.CreateSearchIndexSpecification();
			specification.Filter = "nodealiaspath eq '/example-content'";
			specification.IndexName = "cxlp-kentico-sqki-lp-global-dev";
			specification.Query = "example";


			// Act
			PagedResult<SummaryItem> results = service.Search( specification );



			// Assert
			Assert.IsNotNull( results );
			Assert.IsNotEmpty( results.Items );
			Assert.IsTrue( results.Total == 1 );
			Assert.IsTrue( results.Items.FirstOrDefault()?.Url == "/example-content" );
		}


		[Test]
		public void PagesDocuments( )
		{
			// Arrange
			ISearchIndexSpecification specification = ServiceCreatorUtility.CreateSearchIndexSpecification();
			specification.IndexName = "cxlp-kentico-sqki-lp-global-dev";
			specification.Query = "example";
			specification.PageIndex = 2;
			specification.PageSize = 2;


			// Act
			PagedResult<SummaryItem> results = service.Search( specification );


			// Assert
			Assert.IsNotNull( results );
			Assert.IsNotEmpty( results.Items );

			Assert.GreaterOrEqual( results.TotalPages, 1 );
			Assert.LessOrEqual( results.Items.Count(), specification.PageSize );
			Assert.AreEqual( results.PageIndex, specification.PageIndex );

			Assert.AreEqual( results.RowStart, Math.Max( 0, specification.PageIndex ) * specification.PageSize );
			Assert.GreaterOrEqual( results.RowEnd, results.RowStart + results.Items.Count() - 1 );
		}


		[Test]
		public void ReturnsDocuments( )
		{
			// Arrange
			ISearchIndexSpecification specification = ServiceCreatorUtility.CreateSearchIndexSpecification();
			specification.IndexName = "cxlp-kentico-sqki-lp-global-dev";
			specification.Query = "example";


			// Act
			PagedResult<SummaryItem> results = service.Search( specification );



			// Assert
			Assert.IsNotNull( results );
			Assert.IsNotEmpty( results.Items );
			Assert.IsTrue( results.Items.All( i => !String.IsNullOrWhiteSpace( i.Id ) ), "Some documents had no ID." );
			Assert.IsTrue( results.Items.All( i => !String.IsNullOrWhiteSpace( i.Title ) ), "Some documents had no title." );
			Assert.IsTrue( results.Items.All( i => !String.IsNullOrWhiteSpace( i.Url ) ), "Some documents had no URL." );
		}


		[Test]
		public void ReturnsFacets( )
		{
			// Arrange
			ISearchIndexSpecification specification = ServiceCreatorUtility.CreateSearchIndexSpecification();
			specification.Facets = new string[2] { "documentcategories", "documentcategoryids" };
			specification.IndexName = "cxlp-kentico-sqki-lp-global-dev";
			specification.Query = "example";


			// Act
			PagedResult<SummaryItem> results = service.Search( specification );



			// Assert
			Assert.IsNotNull( results );
			Assert.IsNotEmpty( results.Items );
			Assert.IsNotEmpty( results.Facets );
			Assert.IsTrue( results.Facets.All( f => f.Values.All( fv => fv.Count.HasValue ) ) );
		}


		[Test]
		public async Task UploadsDocuments( )
		{
			// Arrange
			UploadDocumentDto template = new UploadDocumentDto { SysId = "test-001", Title = "Automated Test Document 1", Type = "Test Document", SiteName = "Launchpad", PublishSource = "Automated Tests" };

			List<UploadDocumentDto> documents = new List<UploadDocumentDto>
			{
				template,
				new UploadDocumentDto { SysId = "test-002", Title = "Automated Test Document 2", Type = template.Type, SiteName = template.SiteName, PublishSource = template.PublishSource },
			};


			// Act
			Result result = await service.UploadDocuments( documents, "cxlp-kentico-sqki-lp-global-dev" );


			// Assert
			Assert.IsTrue( result.ResultType == ResultType.Success );
		}


		[Test]
		public void WorksWithEmpty( )
		{
			// Arrange
			ISearchIndexSpecification specification = ServiceCreatorUtility.CreateSearchIndexSpecification();


			// Act
			PagedResult<SummaryItem> results = service.Search( specification );



			// Assert
			Assert.IsNotNull( results );
			Assert.IsEmpty( results.Items );
			Assert.IsNotNull( results.Specification );
			Assert.AreSame( specification, results.Specification );
		}
	}

}
