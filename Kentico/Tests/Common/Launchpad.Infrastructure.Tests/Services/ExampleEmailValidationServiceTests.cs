using CMS.Tests;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class ExampleEmailValidationServiceTests : IntegrationTests
	{
		#region Fields
		private readonly IEmailValidationService service;
		#endregion

		public ExampleEmailValidationServiceTests()
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateEmailValidationService();
		}


		[Test]
		public void ValidateEmail()
		{			
			// This test may fail on usage limits...
			string emailAddress = "";
			bool isValid = false;

			Assert.IsNotNull(emailAddress);
			Assert.IsFalse(isValid);

			//emailAddress = "cx_dev@riseinteractive.com";
			//isValid = service.ValidateEmailAsync(emailAddress).Result;
			//Assert.IsTrue(isValid);

			//emailAddress = "notavalidemailaddress@notavalidemailaddress.com";
			//isValid = service.ValidateEmailAsync(emailAddress).Result;
			//Assert.IsFalse(isValid);

			//emailAddress = "notavalidemailaddressATnotavalidemailaddress.com";
			//isValid = service.ValidateEmailAsync(emailAddress).Result;
			//Assert.IsFalse(isValid);

			//emailAddress = "notavalidemailaddress@notavalidemailaddress";
			//isValid = service.ValidateEmailAsync(emailAddress).Result;
			//Assert.IsFalse(isValid);			
		}		
	}

}
