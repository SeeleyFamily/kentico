using System;
using System.Net;
using Launchpad.Core.Enums;
using Launchpad.Core.Exceptions;


namespace Launchpad.Core.Models
{

	public class ApiResponse<T> : Result
	{
		#region Properties
		public Exception Exception { get; set; }
		public T ResponseObject { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		#endregion


		public ApiResponse()
		{

		}


		public ApiResponse
		( 
			T responseObject, 
			ResultType resultType = ResultType.Success, 
			HttpStatusCode statusCode = HttpStatusCode.OK
		)
		{
			ResponseObject = responseObject;
			ResultType = resultType;
		}


		public ApiResponse
		(
			Exception exception,
			ResultType resultType = ResultType.Error,
			HttpStatusCode statusCode = HttpStatusCode.InternalServerError
		)
		{
			Exception = exception;
			Message = Exception.Message;
			ResultType = resultType;
		}


		public ApiResponse
		(
			ApiException exception,
			ResultType resultType = ResultType.Error
		)
		{
			Exception = exception;
			Message = Exception.Message;
			ResultType = resultType;
			StatusCode = exception.StatusCode;
		}

	}

}
