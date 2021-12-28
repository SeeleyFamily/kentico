using System;
using System.Web;
using CMS.Membership;
using CMS.SiteProvider;
using Kentico.Content.Web.Mvc;
using Kentico.Web.Mvc;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;


namespace Launchpad.Infrastructure.Kentico.Web.Providers
{

	/// <summary>
	/// Provides the current <see cref="IUser"/> from session.
	/// </summary>
	public class CurrentUserProvider : ICurrentUserProvider
	{
		#region Constants
		private const string UserKey = "User";
		#endregion


		#region Fields
		private readonly HttpContextBase httpContext;
		private readonly HttpSessionStateBase session;
		private readonly Lazy<IKenticoUserService> userService;
		#endregion


		#region Properties
		protected IUser User { get; set; }
		#endregion




		public CurrentUserProvider
		(
			HttpContextBase httpContext,
			Lazy<IKenticoUserService> userService
		)
		{
			this.userService = userService;

			if( httpContext != null )
			{
				this.httpContext = httpContext;
				this.session = httpContext.Session;
			}
		}



		/// <summary>
		/// Retrieves the current <see cref="IUser"/> or an <see cref="AnonymousUser"/> if there isn't one.
		/// </summary>
		public virtual IUser GetCurrentUser( )
		{
			if( User == null )
			{
				User = GetUserInternal();
			}

			return User;
		}


		/// <summary>
		/// Sets the current <see cref="IUser"/>.
		/// </summary>
		public virtual void SetCurrentUser( IUser user )
		{
			this.User = user;

			if( session != null )
			{
				session[ UserKey ] = user;
			}
		}



		/// <summary>
		/// Returns an instance of an <see cref="AnonymousUser"/>.
		/// </summary>
		protected virtual IUser GetAnonymousUser( )
		{
			return userService.Value.GetPublicAnonymousUser( SiteContext.CurrentSiteID );
		}


		/// <summary>
		/// Returns an <see cref="IUser"/> mocking the logged in Kentico user during preview requests.
		/// </summary>
		protected virtual IUser GetKenticoPreviewUser( )
		{
			CurrentUserInfo user = MembershipContext.AuthenticatedUser;

			return new User
			{
				Email = user.Email,
				FirstName = user.FirstName,
				IsAuthenticated = true,
				LastName = user.LastName,
				Username = user.UserName
			};
		}


		/// <summary>
		/// Determines an appropriate <see cref="IUser"/> for the current context and request.
		/// </summary>
		protected virtual IUser GetUserInternal( )
		{
			// Retrieve the user from session
			if( session == null || !( session[ UserKey ] is IUser user ) )
			{
				// No session user, is this Kentico preview?
				if( httpContext.Kentico().Preview().Enabled )
				{
					return GetKenticoPreviewUser();
				}


				// Return the anonymous user
				return GetAnonymousUser();
			}


			// Set the user from session and then return it
			User = user;

			return User;
		}

	}

}