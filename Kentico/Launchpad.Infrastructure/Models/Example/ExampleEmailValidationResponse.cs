using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Infrastructure.Models.Example
{
	public class ExampleEmailValidationResponse
	{
		#region fields
		public EmailValidation email_validation { get; set; }
		#endregion

		public class EmailValidation
		{
			public int status_code { get; set; }
			public string address { get; set; }
			public string status { get; set; }
		}
	}
}
