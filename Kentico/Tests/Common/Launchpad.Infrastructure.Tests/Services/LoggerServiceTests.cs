using System;
using CMS.Tests;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class LoggerServiceTests : IntegrationTests
	{
		#region Fields
		private readonly ILoggerService service;
		#endregion


		public LoggerServiceTests( )
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateLoggerService( );
		}



		[Test]
		public void LogsError( )
		{
			// Arrange
			string data = "Simple data object.";



			// Act
			try
			{
				// Kentico's FakeEventLogProvider will consider an error event logged as a test failure, so we capture it in try/catch
				service.LogError( "An Error event has been logged by an integration test.", "TEST", data: data );
			}
			catch( Exception e )
			{
				Assert.IsTrue( e.InnerException?.InnerException?.Message?.Contains( data ), $"Error event did not contain data object ({e.InnerException?.InnerException?.Message})" );
			}


			// Assert
			Assert.Pass();
		}



		[Test]
		public void LogsInformation( )
		{
			// Arrange
			object data = new
			{
				Test = true,
				String = "string test",
				Number = 42,
				Exception = new Exception( "Test object." )
			};


			// Act
			service.LogInformation( "An Information event has been logged by an integration test.", "TEST", data: data );


			// Assert
			Assert.Pass();  // TODO: Logging isn't enabled during tests, another way to determine success other than "didn't error"?
		}



		[Test]
		public void LogsWarning( )
		{
			// Arrange


			// Act
			service.LogWarning( "An Warning event has been logged by an integration test.", "TEST" );


			// Assert
			Assert.Pass();  // TODO: Logging isn't enabled during tests, another way to determine success other than "didn't error"?
		}

	}

}
