using System.Collections.Generic;
using CMS.Base;
using CMS.Tests;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class EmailServiceTests : IntegrationTests
    {
		#region Fields
		private readonly IEmailService service;
		#endregion

		public EmailServiceTests()
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateEmailService();
		}

		[Test]
		public void SendEmail()
		{
			// This test may fail on usage limits...
			string emailTo = "javier.carroz@riseinteractive.com";
			string emailFrom = "hunterscrossalmail@enlivant.com";
			string templateCodeName = "LaunchpadTestEmailTemplateWithMacros";
			Dictionary<string, object> data = new Dictionary<string, object>();
			data.Add("FirstName", "Rise");
			data.Add("LastName", "Developer");
			data.Add("CommunityName", "Rise Offices");
			data.Add("CommunityPhoneNumber", "1-800-LAUNCHPAD");
			string emailSubject = "Launchpad Test: Plain Text Email Body";
			string emailBody = "Email body with plain text.";

			CMSActionContext context = new CMSActionContext
			{
				SendEmails = true
			};

			using (context)
			{
				service.SendEmailFromTemplate(emailTo, emailFrom, templateCodeName, data);
				service.SendEmailFromString(emailTo, emailFrom, 1, emailBody, emailSubject);
			}
		}
	}

}