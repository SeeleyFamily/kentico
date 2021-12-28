using Castle.Core.Internal;
using CMS;
using CMS.MacroEngine;
using CMS.SiteProvider;
using Launchpad.Infrastructure.Kentico.CMS.Macros;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

[assembly: RegisterExtension(typeof(DocumentUrlPathWithLocaleMacro), typeof(string))]
[assembly: RegisterExtension(typeof(DocumentUrlPathWithLocaleMacro), typeof(UtilNamespace))]

namespace Launchpad.Infrastructure.Kentico.CMS.Macros
{
    public class DocumentUrlPathWithLocaleMacro : MacroMethodContainer
    {
        [MacroMethod(typeof(List<string>), "Returns the DocumentUrlPath with a two-letter culture code of current document", 1)]
        [MacroMethodParam(0, "Culture", typeof(string), "Current culture.")]
        [MacroMethodParam(1, "DocumentUrlPath", typeof(string), "Current culture.")]

        public static object GetDocumentUrlPathWithLocale(EvaluationContext context, params object[] parameters)
        {
            // Branches according to the number of the method's parameters
            switch (parameters.Length)
            {
                case 2:
                    return FormUrl(parameters[0].ToString(), parameters[1].ToString());
                case 3:
                    // Weird bug causing macro expression in url pattern to have two parameters where the first parameter is null.
                    return FormUrl(parameters[1].ToString(), parameters[2].ToString());
                default:
                    // No other overloads are supported
                    return string.Empty;
            }
        }

        private static string FormUrl(string cultureCode, string documentUrlPath)
        {
            var culture = CultureSiteInfoProvider.GetSiteCultures(SiteContext.CurrentSiteName).Items.Where(x => x.CultureCode == cultureCode).FirstOrDefault();
            if (culture != null)
            {
                if (!culture.CultureAlias.IsNullOrEmpty())
                {
                    //we're storing the culture code on save but 
                    if (documentUrlPath.StartsWith(culture.CultureAlias, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return $"{documentUrlPath}";
                    }
                    else
                    {
                        return $"{culture.CultureAlias}{documentUrlPath}";

                    }
                }
                else 
                {
                    return $"{documentUrlPath}";
                }
            }
            else
            {
                return $"{documentUrlPath}";
            }
        }
    }
}
