using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Exceptions;
using Launchpad.Core.Models;
using Newtonsoft.Json;


namespace Launchpad.Infrastructure.Services
{

	public abstract class ExternalApiService
	{
		#region Properties
		protected virtual string ApiBaseUrl { get; set; }
		protected virtual double LogAfterSeconds { get; set; }
		protected virtual string Password { get; set; }
		protected virtual string Token { get; set; }
		protected virtual string Username { get; set; }
		#endregion


		#region Fields
		/// <remarks>
		/// Receive the <see cref="HttpClient"/> from DI so that lifetime can be controlled by the application. In
		/// most cases, this should be a single HttpClient per application, rather than per-use, for performance
		/// and stability reasons. See https://docs.microsoft.com/en-us/azure/architecture/antipatterns/improper-instantiation/.
		/// </remarks>
		private readonly HttpClient httpClient;

		private readonly Lazy<ILoggerService> loggerService;
		#endregion


		public ExternalApiService
		(
			HttpClient httpClient,
			Lazy<ILoggerService> loggerService
		)
		{
			this.httpClient = httpClient;
			this.loggerService = loggerService;
		}



		protected virtual ApiResponse<T> CreateErrorResponse<T>( ApiException exception )
		{
			ApiResponse<T> response = new ApiResponse<T>( exception );

			return response;
		}


		protected virtual HttpRequestMessage CreateRequestMessage( HttpMethod method, string endpoint )
		{
			Uri baseUri = new Uri( ApiBaseUrl, UriKind.Absolute );
			Uri requestUri = new Uri( baseUri, endpoint );

			HttpRequestMessage request = new HttpRequestMessage( method, requestUri );


			// Add authorization if provided
			if( !String.IsNullOrWhiteSpace( Token ) )
			{
				// Bearer token (JWT)
				request.Headers.Authorization = new AuthenticationHeaderValue( "Bearer", Token );
			}
			else if( !String.IsNullOrWhiteSpace( Username ) || !String.IsNullOrWhiteSpace( Password ) )
			{
				// Basic username:password
				string basicAuthString;
				if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
				{
					// Either Username or Password is empty // whitespace.
					// Concat together without :
					basicAuthString = Username.Trim() + Password.Trim();
				}
				else
				{
					basicAuthString = Username + ":" + Password;
				}
				

				string parameter = Convert.ToBase64String( Encoding.Default.GetBytes( basicAuthString ) );
				request.Headers.Authorization = new AuthenticationHeaderValue( "Basic", parameter );
			}


			return request;
		}


		protected virtual ApiResponse<T> CreateResponse<T>( T responseObject )
		{
			ApiResponse<T> response = new ApiResponse<T>( responseObject );

			return response;
		}


		protected virtual async Task<ApiResponse<T>> GetWebResponseAsync<T>( string endpoint )
		{
			HttpRequestMessage request = CreateRequestMessage( HttpMethod.Get, endpoint );


			return await SendRequestAsync<T>( request );
		}


		protected virtual async Task<ApiResponse<T>> PostWebRequestAsync<T>( string endpoint, object data )
		{
			string json = JsonConvert.SerializeObject( data );

			HttpRequestMessage request = CreateRequestMessage( HttpMethod.Post, endpoint );
			request.Content = new StringContent( json, Encoding.UTF8, "application/json" );


			return await SendRequestAsync<T>( request );
		}


		protected virtual async Task<ApiResponse<T>> SendRequestAsync<T>( HttpRequestMessage request )
		{
			// Track the time the call incurs
			Stopwatch timer = new Stopwatch();
			timer.Start();


			try
			{
				// Make the call
				using( HttpResponseMessage responseMessage = await httpClient.SendAsync( request ) )
				{
					if( responseMessage.IsSuccessStatusCode )
					{
						string responseJson = await responseMessage.Content.ReadAsStringAsync();

						return CreateResponse( JsonConvert.DeserializeObject<T>( responseJson ) );
					}


					// Throw an API error and include the resulting content, if any
					string content = null;

					if( responseMessage.Content != null && responseMessage.Content.Headers.ContentLength > 0 )
					{
						content = await responseMessage.Content.ReadAsStringAsync();
					}

					throw new ApiException( responseMessage, content );
				}
			}
			catch( ApiException e )
			{
				loggerService.Value.LogError( $"{request.RequestUri} responded with an error:", "API ERROR", data: request.Content, exception: e );
				return CreateErrorResponse<T>( e );
			}

			finally
			{
				// Housekeeping on timer related logging
				timer.Stop();


				if( timer.ElapsedMilliseconds > LogAfterSeconds * 1000 )
				{
					loggerService.Value.LogWarning( $"{request.RequestUri} API call took {timer.ElapsedMilliseconds / 1000}s to perform.", "API TIMER", data: request.Content );
				}
			}
		}

	}

}