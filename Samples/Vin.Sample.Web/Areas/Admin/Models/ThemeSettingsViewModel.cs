using System.Collections.Generic;
using Vin.Core.Model.MultiTenancy;
using Vin.Core.Model.Themes;

namespace Vin.Sample.Web.Areas.Admin.Models
{
    public class ThemeSettingsViewModel
    {
        public ThemeConfiguration CurrentTheme { get; set; }

        public IList<ThemeConfiguration> ThemeConfigs { get; set; }
    }
}