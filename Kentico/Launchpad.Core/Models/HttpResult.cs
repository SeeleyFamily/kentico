using System.Net;
using System.Net.Http;


namespace Launchpad.Core.Models
{

	public class HttpResult : Result
	{
		#region Properties
		public bool IsSuccessStatusCode { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		#endregion



		public HttpResult( )
		{

		}


		public HttpResult( HttpResponseMessage response )
		{
			this.IsSuccessStatusCode = response.IsSuccessStatusCode;
			this.StatusCode = response.StatusCode;

			this.Message = response.Content.ReadAsStringAsync().Result;
			this.ResultType = response.IsSuccessStatusCode ? Enums.ResultType.Success : Enums.ResultType.Error;
		}
	}

}