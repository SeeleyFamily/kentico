using System.Threading.Tasks;


namespace Launchpad.Core.Abstractions.Services
{

	public interface IEmailValidationService
	{
		/// <summary>
		/// This method takes an input Email Address and returns true if the email is valid // false if the email is invalid
		/// </summary>		
		Task<bool> ValidateEmailAsync(string emailAddress);
	}

}
