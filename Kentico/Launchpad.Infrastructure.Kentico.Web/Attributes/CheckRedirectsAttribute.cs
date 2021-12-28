using Launchpad.Core.Enums;
using Launchpad.Infrastructure.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Launchpad.Infrastructure.Kentico.Web.Attributes
{
	public class CheckRedirectsAttribute : ActionFilterAttribute
	{
		private IRedirectService RedirectService { get; set; }

		public CheckRedirectsAttribute()
		{
			RedirectService = DependencyResolver.Current.GetService<IRedirectService>();
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!string.IsNullOrWhiteSpace(filterContext.HttpContext.Request.Url.Query))
			{
				var pathAndQuery = filterContext.HttpContext.Request.Url.PathAndQuery;
				var redirect = RedirectService.MatchRedirect(pathAndQuery, RedirectMode.PathAndQuery);
				var redirectType = RedirectType.Permanent;
				var redirectUrl = pathAndQuery.TrimStart('/');
				if (redirect != null)
				{
					if (redirect.IsRegexMatch)
					{
						if (redirect.IsRegexReplace)
						{

							redirectUrl = redirect.RegexRule.Replace(redirectUrl, redirect.RedirectURL);
						}
						else
						{
							redirectUrl = redirect.RedirectURL;
						}
					}
					else
					{
						if (redirect.IsTemporaryRedirect)
						{
							redirectType = RedirectType.Temporary;
						}
						redirectUrl = redirect.RedirectURL;
					}

					filterContext.Result = new RedirectResult(redirectUrl, redirectType == RedirectType.Permanent);
					return;
				}
			}
			base.OnActionExecuting(filterContext);
		}
	}
}
