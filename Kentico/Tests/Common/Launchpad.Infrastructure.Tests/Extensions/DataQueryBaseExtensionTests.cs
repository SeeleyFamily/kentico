using System.Linq;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Tests;
using Launchpad.Infrastructure.Extensions;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class DataQueryBaseExtensionTests : IntegrationTests
	{

		[Test]
		public void ExecutesCommonTableExpressions( )
		{
			// Arrange
			IDataQuery cte = DocumentHelper.GetDocuments()
										   .TopN( 10 )
										   .Columns( "NodeID", "NodeGUID", "DocumentName" );


			// Act
			MultiDocumentQuery query = DocumentHelper.GetDocuments()
													 .WithCte( cte )
													 .Source( qs => qs.InnerJoin( "cte", "SubData.NodeID", "cte.NodeID" ) );

			var result = query.TypedResult;


			// Assert
			Assert.IsNotNull( query );
			Assert.IsNotNull( result );
			Assert.IsNotEmpty( result );
			Assert.LessOrEqual( result.Count(), 10 );
			Assert.IsTrue( query.QueryText.Contains( "WITH" ) );
		}


		[Test]
		public void HandlesMultipleCtes( )
		{
			// Arrange
			IDataQuery cte1 = DocumentHelper.GetDocuments()
											.TopN( 1 )
											.Columns( "NodeID", "NodeGUID", "DocumentName" );

			IDataQuery cte2 = DocumentHelper.GetDocuments()
											.TopN( 2 )
											.Columns( "NodeID", "NodeGUID", "DocumentName" );


			// Act
			MultiDocumentQuery query = DocumentHelper.GetDocuments()
													 .WithCte( cte1, "cte1" )
													 .WithCte( cte2, "cte2" )
													 .Where( "NodeID IN ( SELECT NodeID FROM cte1 )" )
													 .Or()
													 .Where( "NodeID IN ( SELECT NodeID FROM cte2 )" );

			var result = query.TypedResult;


			// Assert
			Assert.IsNotNull( query );
			Assert.IsNotNull( result );
			Assert.IsNotEmpty( result );
			Assert.AreEqual( result.Count(), 2 );
			Assert.IsTrue( query.QueryText.Contains( "WITH" ) );
		}

	}

}