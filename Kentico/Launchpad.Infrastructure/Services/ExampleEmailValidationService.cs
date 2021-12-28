using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.Models.Example;


namespace Launchpad.Infrastructure.Services
{

	public class ExampleEmailValidationService : ExternalApiService, IEmailValidationService, IPerApplicationService
	{	

		private string ApiKey { get; set; }

		/// <summary>
		/// <see cref="https://docs.towerdata.com"/>
		/// </summary>		
		public ExampleEmailValidationService
		(
			HttpClient httpClient,
			Lazy<ILoggerService> loggerService
		) : base(httpClient, loggerService)
		{
			this.ApiBaseUrl = ConfigurationManager.AppSettings.Get("api:emailvalidationservice:baseurl");
			this.ApiKey = ConfigurationManager.AppSettings.Get("api:emailvalidationservice:apikey");
			httpClient.Timeout = TimeSpan.FromSeconds(10);
		}
		


		public async Task<bool> ValidateEmailAsync(string emailAddress)
		{		
			var isValid = false;

			try
			{
				var response = await GetWebResponseAsync<ExampleEmailValidationResponse>($"/v5/ev?email={emailAddress}&api_key={ApiKey}");
				ExampleEmailValidationResponse emailValidationResponse = response.ResponseObject;
				isValid = (emailValidationResponse.email_validation.status_code == 50);
			}
			catch (Exception)
			{				
				// TODO LOG
			}

			return isValid;
		}		
	}

}
