using Launchpad.Core.Models;
using System.Collections.Generic;


namespace Launchpad.Core.Abstractions.Models
{

	/// <summary>
	/// Provides an interface defining a common User model.
	/// </summary>
	public interface IUser
	{
		/// <summary>
		/// List of <see cref="AccessControlItem"/> items defining allowed/denied access to objects.
		/// </summary>
		IEnumerable<AccessControlItem> AccessControlList { get; }

		string Email { get; }
		string FirstName { get; }
		bool IsAuthenticated { get; }
		string LastName { get; }
		IEnumerable<Role> Roles { get; }
		int UserId { get; }
		string Username { get; }
	}

}