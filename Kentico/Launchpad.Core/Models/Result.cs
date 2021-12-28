using Launchpad.Core.Enums;


namespace Launchpad.Core.Models
{

	public class Result
	{
		#region Properties
		public ResultType ResultType { get; set; }
		public string Message { get; set; }
		#endregion


		public Result( )
		{

		}


		public Result( ResultType type )
		{
			ResultType = type;
		}


		public Result( ResultType type, string message )
		{
			Message = message;
			ResultType = type;
		}
	}

}
