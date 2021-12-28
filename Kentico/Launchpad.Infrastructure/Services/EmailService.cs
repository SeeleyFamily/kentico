using CMS.DataEngine;
using CMS.EmailEngine;
using CMS.MacroEngine;
using CMS.SiteProvider;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Infrastructure.Services
{
	public class EmailService : IEmailService, IPerApplicationService
	{
		private readonly IEmailTemplateInfoProvider emailTemplateInfoProvider;
		private readonly Lazy<ILoggerService> loggerService;

		public EmailService
		(
			Lazy<ILoggerService> loggerService
		)
		{
			this.emailTemplateInfoProvider = new EmailTemplateProvider();
			this.loggerService = loggerService;
		}

		public bool SendEmailFromTemplate(string emailTo, string emailFrom, string templateCodeName, IDictionary<string, object> data = null)
		{
			// Get the template
			EmailTemplateInfo template = emailTemplateInfoProvider.Get().WhereEquals("EmailTemplateName", templateCodeName).FirstOrDefault();

			if (template == null)
			{
				LogError($"Could not retrieve EmailTemplate '{templateCodeName}'.");
				return false;
			}

			// Use the template to get the site
			SiteInfo siteInfoData = SiteInfo.Provider.Get(template.TemplateSiteID);

			if (siteInfoData == null)
			{
				LogError($"Cannot retrieve site.", templateCodeName);
				return false;
			}

			//Define some of the Email properties before its declaration.
			string emailBody = !string.IsNullOrEmpty(template.TemplateText) ? template.TemplateText : template.TemplatePlainText;
			string messageFrom = !string.IsNullOrEmpty(emailFrom) ? emailFrom : template.TemplateFrom;

			if (string.IsNullOrEmpty(messageFrom))
			{
				LogError($"emailFrom cannot be blank or null.", messageFrom);
				return false;
			}

			// Construct email message
			EmailMessage message = new EmailMessage
			{
				EmailFormat = EmailFormatEnum.Html,
				Recipients = emailTo,
				Body = emailBody,
				From = messageFrom,
				ReplyTo = messageFrom,
				Subject = template.TemplateSubject
			};

			// Configure macro replacements
			MacroResolver macroResolver = MacroResolver.GetInstance();
			if (data != null && data.Any())
			{
				macroResolver.SetNamedSourceData(data);
			}

			try
			{
				EmailSender.SendEmail(siteInfoData.SiteName, message, templateCodeName, macroResolver, sendImmediately: true);
				return true;
			}
			catch (Exception e)
			{
				LogError(e.Message, templateCodeName);
				return false;
			}
		}

		public bool SendEmailFromString(string emailTo, string emailFrom, SiteInfoIdentifier siteIdentifier, string emailBody, string emailSubject)
		{
			// Use the siteIndentifier value to get the site
			SiteInfo siteInfoData = SiteInfo.Provider.Get(siteIdentifier.ObjectID);

			if (siteInfoData == null)
			{
				LogError($"Cannot retrieve site.", siteIdentifier);
				return false;
			}

			if (string.IsNullOrEmpty(emailFrom))
			{
				LogError($"emailFrom cannot be blank or null.", emailFrom);
				return false;
			}

			// Construct email message
			EmailMessage message = new EmailMessage
			{
				EmailFormat = EmailFormatEnum.Html,
				Recipients = emailTo,
				Body = emailBody,
				PlainTextBody = emailBody,
				From = emailFrom,
				ReplyTo = emailFrom,
				Subject = emailSubject
			};

			try
			{
				EmailSender.SendEmail(siteInfoData.SiteName, message, sendImmediately: true);
				return true;
			}
			catch (Exception e)
			{
				LogError(e.Message);
				return false;
			}
		}

		private void LogError(string description, string templateCodeName = null)
		{
			loggerService.Value.LogError($"Template: {templateCodeName} errored with: {description}", "EmailError" );
		}
	}
}
