using System;
using System.Text.RegularExpressions;

namespace Launchpad.Core.Models
{
    public class Redirect
    {      
        public string MatchURL { get; set; }
        public string RedirectURL { get; set; }
        public bool IsTemporaryRedirect { get; set; }        
        public Regex RegexRule { get; set; }
        public bool IsRegexMatch { get; set; }
        public bool IsRegexReplace { get; set; }
        public int Priority { get; set; }
        public bool IsValid
        {
            get
            {
                bool isValid = true;
                try
                {
                    Validate();
                }
                catch (Exception)
                {
                    // Invalid
                    isValid = false;
                }
                return isValid;
            }
        }
        
        public Redirect(string matchURL, string redirectURL, bool isTemporaryRedirect = false, bool isRegexMatch = false, bool isRegexReplace = false, int priority = 0)
        {
            MatchURL = matchURL.Trim();
            RedirectURL = redirectURL.Trim();
            IsTemporaryRedirect = isTemporaryRedirect;
            IsRegexMatch = isRegexMatch;
            IsRegexReplace = isRegexReplace;
            Priority = priority;
            Validate();
        }
        
        /// <summary>
        /// Throws an exception if the Redirect is not a valid redirect
        /// </summary>        
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(MatchURL))
            {
                throw new Exception("Match URL can not be empty.");
            }
            if (string.IsNullOrWhiteSpace(RedirectURL))
            {
                throw new Exception("Redirect URL can not be empty.");
            }
            if (IsRegexMatch)
            {
                try
                {
                    RegexRule = new Regex(MatchURL);
                }
                catch (Exception)
                {
                    throw new Exception($"The Regex pattern is invalid.");
                }
            }
            else
            {
                if (!Uri.IsWellFormedUriString(MatchURL, UriKind.Relative))
                {
                    throw new Exception("Match URL must be a valid Relative URL.");
                }
                if (!MatchURL.StartsWith("/"))
                {
                    throw new Exception("Match URL must start with a /");
                }
            }
            if (!IsRegexReplace)
            {
                if (!Uri.IsWellFormedUriString(RedirectURL, UriKind.RelativeOrAbsolute))
                {
                    throw new Exception("Redirect URL must be a valid Relative Or Absolute URL.");
                }
				if (Uri.IsWellFormedUriString(RedirectURL, UriKind.Relative) && !RedirectURL.StartsWith("/"))
				{
					throw new Exception("Redirect URL must start with a /");
				}
			}
		}       
    }
}
