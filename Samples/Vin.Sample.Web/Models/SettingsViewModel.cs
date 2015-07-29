using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vin.Sample.Data.Model.Settings;

namespace Vin.Sample.Web.Models
{
    public class SettingsViewModel
    {
        public CompanySettings Company { get; set; }

        public SiteSettings Site { get; set; }

        public SocialSettings Social { get; set; }
    }
}