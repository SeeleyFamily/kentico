using Launchpad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Launchpad.Core.Extensions
{

	public static class StringExtensions
	{

		public static string AddUrlParameter(this string url, string parameter, string value)
		{
			var urlParts = url.Split('?');
			if (urlParts.Length <= 1)
			{
				return $"{url}?{parameter}={value}";
			}
			else
			{
				var qsParts = urlParts[1].Split('&').ToList();
				if (qsParts.Any(x => x.ToLower().Contains($"{parameter}=")))
				{
					qsParts.ForEach(x =>
					{
						if (x.ToLower().Contains($"{parameter}="))
						{
							x = $"{parameter}={value}";
						}
					});
					return $"{url}?{string.Join("&", qsParts)}";
				}
				else
				{
					return $"{url}&{parameter}={value}";
				}
			}
		}


		public static Dictionary<string, string> GenerateOptionsDictionary(this string input)
		{
			if (String.IsNullOrEmpty(input))
			{
				input = "";
			}


			Dictionary<string, string> opts = new Dictionary<string, string>();
			string[] optItems = input.Split(
				new[] { "\r\n", "\r", "\n" },
				StringSplitOptions.None
			);


			foreach (string opt in optItems)
			{
				if (String.IsNullOrEmpty(opt.Trim()))
				{
					continue;
				}
				if (opt.IndexOf(';') > -1)
				{
					string[] parts = opt.Split(';');
					opts.Add(parts[0], parts[1]);
				}
				else
				{
					opts.Add(opt, opt);
				}
			}

			return opts;
		}


		public static string GetOptimizedImageUrl(this string url, string format = "webp")
		{
			if (string.IsNullOrWhiteSpace(url))
			{
				return string.Empty;
			}

			url = url.SanitizeMediaUrl();
			if (url.ToLower().Contains("/getmedia/"))
			{
				if (!url.Contains("?"))
				{
					url = url + "?";
				}
				else
				{
					url = url + "&";
				}
				url = "/optimize" + url + "format=" + format;
			}
			return url;
		}


		public static string GetParentPath(this string nodeAliasPath)
		{
			var segments = nodeAliasPath.Trim().Split('/');
			var parentSegments = segments.Take(segments.Count() - 1);
			var parentPath = string.Join("/", parentSegments);
			if (string.IsNullOrWhiteSpace(parentPath) && !string.IsNullOrWhiteSpace(nodeAliasPath) && nodeAliasPath.Trim() != "/")
			{
				return "/";
			}
			return parentPath;
		}


		public static string GetUriHost(this string url)
		{
			try
			{
				Uri uri = new Uri(url);
				return uri.Host;
			}
			catch (Exception)
			{
				return url;
			}
		}


		public static string SanitizeMediaUrl(this string url)
		{
			if (string.IsNullOrWhiteSpace(url))
			{
				return string.Empty;
			}

			if (url.StartsWith("~/"))
			{
				url = url.TrimStart('~');
			}
			return url;
		}


		public static string StripHTML(this string input)
		{
			return Regex.Replace(input, "<[a-zA-Z/].*?>|&nbsp;", String.Empty);
		}


		public static Guid[] ToGuidArray(this string value, char delimiter = ',')
		{
			if (String.IsNullOrWhiteSpace(value))
			{
				return new Guid[0];
			}


			List<Guid> guids = new List<Guid>();

			foreach (string item in value.Split(delimiter))
			{
				if (Guid.TryParse(item, out Guid result))
				{
					guids.Add(result);
				}
			}


			return guids.ToArray();
		}


		public static string TruncateWithEllipsis(this string value, int maxChars)
		{
			if (value != null)
			{
				return value.Length <= maxChars ? value : value.Substring(0, maxChars - 3) + "...";
			}
			else
			{
				return value;
			}
		}

		public static string TruncateToWordWithEllipsis(this string value, int maxChars)
        {
			if(!String.IsNullOrEmpty(value)) 
            {
				var simpleTruncate = value.Length <= maxChars ? value : value.Substring(0, maxChars - 3);
				var positionOfLastSpace = simpleTruncate.LastIndexOf(" ");
				return value.Length <= positionOfLastSpace ? value : value.Substring(0, positionOfLastSpace) + "...";
			}
			else
            {
				return value;
            }
		}

		/// <summary>
		/// This extension method returns a truncated string with length equal to the position of the first space OR period after
		/// <paramref name="characterCountToTruncateAfter"/> characters. 
		/// This method is fundamentally differ from <see cref="TruncateWithEllipsis(string, int)"/> and <see cref="TruncateToWordWithEllipsis(string, int)"/>
		/// in that it ensures the truncated string is at least <paramref name="characterCountToTruncateAfter"/> characters in length. Other
		/// aforementioned methods limit truncated string to no more than maxChars.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="characterCountToTruncateAfter"></param>
		/// <param name="withEllipsis"></param>
		/// <returns></returns>
		public static string TruncateToFirstPeriodOrSpaceAfterCharacterCount(this string value, int characterCountToTruncateAfter, bool withEllipsis = true)
        {
			if (!string.IsNullOrWhiteSpace(value) && value.Length > characterCountToTruncateAfter)
			{
				var lastSpaceIndex = value.IndexOf(' ', characterCountToTruncateAfter);
				var lastPeriodIndex = value.IndexOf('.', characterCountToTruncateAfter);

				// if index is -1 (i.e. character not found in string) then set lastThingIndex to length of the original string
				lastSpaceIndex = lastSpaceIndex == -1 ? value.Length : lastSpaceIndex;
				lastPeriodIndex = lastPeriodIndex == -1 ? value.Length : lastSpaceIndex;

				var length = Math.Min(lastSpaceIndex, lastPeriodIndex);
				return $"{value.Substring(0, length)}{(withEllipsis ? "..." : "")}";
			}

			return value;
		}

		public static string ConvertToAbsoluteUrl(this string url, string scheme, string authority)
		{
			if (!string.IsNullOrWhiteSpace(url))
			{
				if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
				{
					return url;
				}

				return $"{scheme}://{authority}{url.TrimStart('~')}";
			}

			return string.Empty;
		}

		public static string WrapInParagraph(this string content)
		{
			if (!string.IsNullOrWhiteSpace(content))
			{
				content = content.Trim();
				if (!content.StartsWith("<p>") && !content.EndsWith("</p>"))
				{
					content = $"<p>{content}</p>";
				}
			}
			return content;
		}

		/// Breadcrumb string is seperated by pipe symbol(|)
		/// Item left to colon (:) is document name and Item right to colon(:) is document url path
		public static IEnumerable<Breadcrumb> ToBreadcrumbModel(this string crumbs)
		{
			if (string.IsNullOrEmpty(crumbs))
			{
				return null;
			}

			string[] result = crumbs.Split('|');

			if (result == null)
			{
				return null;
			}

			if (result.Count() > 0)
			{
				List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();

				foreach (var item in result)
				{
					breadcrumbs.Add(new Breadcrumb
					{
						Title = item.Split(':')[0],
						Url = item.Split(':')[1]
					});
				}

				return breadcrumbs ?? null;
			}

			return null;
		}

	}
}
