using System.Collections.Generic;
using Launchpad.Core.Abstractions.Models;


namespace Launchpad.Core.Models
{

	/// <summary>
	/// Model representing a user of the system.
	/// </summary>
	public class User : IUser
	{
		public virtual IEnumerable<AccessControlItem> AccessControlList { get; set; }
		public virtual string Email { get; set; }
		public virtual string FirstName { get; set; }
		public virtual bool IsAuthenticated { get; set; }
		public virtual string LastName { get; set; }
		public IEnumerable<Role> Roles { get; set; }
		public virtual int UserId { get; set; }
		public virtual string Username { get; set; }

	}

}
