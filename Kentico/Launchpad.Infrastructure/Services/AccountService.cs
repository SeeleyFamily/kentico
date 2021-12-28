using CMS.DataEngine;
using CMS.Membership;
using CMS.SiteProvider;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Launchpad.Infrastructure.Services
{

	public class AccountService : IAccountService, IKenticoUserService, IPerApplicationService
	{
		#region Fields
		private const string AuthorizedUsersRoleName = "AuthorizedUsers";
		private readonly Lazy<ICacheService> cacheService;
		#endregion


		public AccountService
		(
			Lazy<ICacheService> cacheService
		)
		{
			this.cacheService = cacheService;
		}



		public virtual UserInfo EnsureUser(IUser user, SiteInfo siteInfo)
		{
			// Find the user in Kentico
			UserInfo userInfo = UserInfo.Provider.Get(user.Username);

			if (userInfo != null)
			{
				// User exists; update roles and move on
				EnsureRoles(user, userInfo, siteInfo);
				return userInfo;
			}


			// Create a Kentico user
			userInfo = new UserInfo
			{
				Email = user.Email,
				Enabled = true,
				FirstName = user.FirstName,
				FullName = $"{user.FirstName} {user.LastName}",
				IsExternal = true,
				LastName = user.LastName,
				PreferredCultureCode = "en-us",
				UserCreated = DateTime.Now,
				UserDescription = $"Imported user with External ID {{{user.UserId}}}.",
				UserIsExternal = true,
				UserName = user.Username
			};

			userInfo.SetValue("ExternalID", user.UserId);


			// Save the Kentico user
			UserInfo.Provider.Set(userInfo);

			// Update the user's site
			UserInfoProvider.AddUserToSite(userInfo.UserName, siteInfo.SiteName);

			// Update the user's roles
			EnsureRoles(user, userInfo, siteInfo);


			return userInfo;
		}


		public virtual IEnumerable<Role> GetRoles(int userId)
		{
			throw new NotImplementedException();
		}


		public virtual AnonymousUser GetPublicAnonymousUser(int siteId)
		{
			// Create the anonymous user and cache it for later retrieval
			AnonymousUser result = cacheService.Value.GetFromCache<AnonymousUser>(cs =>
		   {
				// Get the Public user
				UserInfo userInfo = UserInfo.Provider.Get("public");

			   if (userInfo == null)
			   {
				   throw new Exception("There is no public user.");
			   }


				// Create an AnonymousUser with these details
				AnonymousUser user = new AnonymousUser
			   {
				   AccessControlList = GetAcl(userInfo.UserID, siteId),
				   FirstName = userInfo.FirstName,
				   IsAuthenticated = false,
				   LastName = userInfo.LastName,
				   UserId = userInfo.UserID,
				   Username = userInfo.UserName
			   };

			   return user;
		   },
				$"users|anonymous",
				new string[] { "cms.aclitem|all" });


			// Return the anonymous user
			return result;
		}


		public virtual IUser GetUser(int userId)
		{
			throw new NotImplementedException();
		}


		public virtual void SetAcl(IUser user, int userId, int siteId)
		{
			((User)user).AccessControlList = GetAcl(userId, siteId);
		}



		protected virtual void EnsureRoles(IUser user, UserInfo userInfo, SiteInfo siteInfo)
		{
			if (user.Roles != null && user.Roles.Any())
			{
				ObjectQuery<RoleInfo> roles = RoleInfo.Provider.Get()
															  .Columns("RoleID", "RoleName")
															  .WhereEquals("SiteID", siteInfo.SiteID)
															  .WhereIn("RoleName", user.Roles.Select(r => r.ID.ToString()).ToArray());

				foreach (RoleInfo role in roles)
				{
					if (!userInfo.IsInRole(role.RoleName, siteInfo.SiteName))
					{
						UserInfoProvider.AddUserToRole(userInfo.UserID, role.RoleID);
					}
				}
			}


			// All users receive "Authorized Users" role
			if (!userInfo.IsInRole(AuthorizedUsersRoleName, siteInfo.SiteName))
			{
				UserInfoProvider.AddUserToRole(userInfo.UserName, AuthorizedUsersRoleName, siteInfo.SiteName);
			}
		}


		protected virtual IEnumerable<AccessControlItem> GetAcl(int userId, int siteId)
		{
			string columns = "Allowed, Denied, ACLID";


			// User/everyone query
			DataQuery query1 = new DataQuery().Distinct()
											  .Columns(columns)
											  .From("View_Custom_Acl_Items_Expanded V")
											  .Where("V.UserID IN (SELECT CMS_User.UserID FROM CMS_User JOIN CMS_UserSite ON CMS_User.UserID = CMS_UserSite.UserID WHERE CMS_User.UserID = @UserID AND CMS_UserSite.SiteID = @SiteID )")
											  .Or()
											  .Where("V.RoleID IN (SELECT RoleID FROM CMS_Role WHERE RoleName IN ('_everyone_','_notauthenticated_'))");

			// Membership/roles query
			DataQuery query2 = new DataQuery().Distinct()
											  .Columns(columns)
											  .From("View_Custom_Acl_Items_Expanded V")
											  .Source(qs => qs.InnerJoin("View_CMS_UserRoleMembershipRole AS UserRole", "UserRole.RoleID", "V.RoleID"))
											  .Where("(UserRole.SiteID = @SiteID OR UserRole.SiteID IS NULL)")
											  .Where("UserRole.UserID = @UserID");

			// Union of the two queries for the full ACL applicable to the user
			DataQuery aclQuery = query1.Union(query2);

			aclQuery.EnsureParameters();
			aclQuery.Parameters.Add("@UserID", userId, typeof(int));
			aclQuery.Parameters.Add("@SiteID", siteId, typeof(int));

			DataSet result = aclQuery.Result;
			List<AccessControlItem> acl = new List<AccessControlItem>();

			if (!(result?.Tables[0]?.Rows is DataRowCollection rows) || rows.Count == 0)
			{
				return acl;
			}


			// Add the ACL Items to the list
			foreach (DataRow row in rows)
			{
				acl.Add(new AccessControlItem
				{
					AclId = row.Field<int>("AclID"),
					IsAllowed = ((row.Field<int>("Allowed") & 1) != 0),   // Allowed flag is set
					IsDenied = ((row.Field<int>("Denied") & 1) != 0)      // Deny flag is set
				});
			}


			return acl;
		}

	}

}
