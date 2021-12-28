


namespace Launchpad.Core.Models
{

	/// <summary>
	/// Class representing an anonymous, not-logged in <see cref="User"/>.
	/// </summary>
	public class AnonymousUser : User
	{
		public override string Email => null;
		public override string FirstName => "Anonymous";
		public override bool IsAuthenticated => false;
		public override string LastName => "User";
		public override string Username => null;
	}

}
