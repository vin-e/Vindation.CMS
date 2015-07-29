using Vin.Core.Model.MultiTenancy;
using Vin.Sample.Data.Model.Settings;

namespace Vin.Sample.Web.Areas.Admin.Models
{
    public class SiteSettingsViewModel
    {
        public SiteSettings SiteSettings { get; set; }

        public TenantSettings TenantSettings { get; set; }
    }
}