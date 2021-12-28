using System;
using System.Net;
using System.Net.Http;


namespace Launchpad.Core.Exceptions
{

	public class ApiException : Exception
	{
		#region Properties
		public string Content { get; protected set; }
		public HttpResponseMessage ResponseMessage { get; protected set; }
		public override string Message { get => $"API request {ResponseMessage.RequestMessage.RequestUri} returned {( int ) ResponseMessage.StatusCode} {ResponseMessage.ReasonPhrase}."; }
		public HttpStatusCode StatusCode { get; set; }
		#endregion


		public ApiException( HttpResponseMessage responseMessage )
		{
			ResponseMessage = responseMessage;
			StatusCode = responseMessage.StatusCode;
		}


		public ApiException( HttpResponseMessage responseMessage, string content )
		{
			Content = content;
			ResponseMessage = responseMessage;
			StatusCode = responseMessage.StatusCode;
		}

	}

}
