using CMS.DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Core.Abstractions.Services
{
    public interface IEmailService
    {
        bool SendEmailFromTemplate(string emailTo, string emailFrom, string templateCodeName, IDictionary<string, object> data = null);
        bool SendEmailFromString(string emailTo, string emailFrom, SiteInfoIdentifier siteIdentifier, string emailBody, string emailSubject);
    }
}
