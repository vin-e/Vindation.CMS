using Autofac;
using Autofac.Integration.Owin;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using Vin.Core.Model.Themes;
using Vin.Core.MultiTenancy;
using Vin.Core.Services.Themes;

namespace Vin.Core.Model.MultiTenancy
{
    /// <summary>
    /// Tenant
    /// </summary>
    public class Tenant : BaseIdEntity
    {
        /// <summary>
        /// Gets or sets the name of the Tenant
        /// </summary>
        [MaxLength(255)]
        public virtual string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the identifiers of the tenant
        /// </summary>
        public virtual ICollection<TenantIdentifier> RequestIdentifiers { get; set; }

        private ThemeConfiguration _themeConfig { get; set; }

        /// <summary>
        /// Gets or sets the session theme configuration for the tenant
        /// </summary>
        [NotMapped]
        public ThemeConfiguration ThemeConfig { 
            get {
                if (_themeConfig != null)
                    return _themeConfig;

                var context = HttpContext.Current.GetOwinContext();
                var lifetimeScope = context.GetAutofacLifetimeScope();
                var tenantInstance = context.GetTenantInstance();
                var _themeService = lifetimeScope.Resolve<IThemeService>();
                _themeConfig = _themeService.GetTenantThemeConfiguration(tenantInstance.Tenant);

                return _themeConfig;
            }
            set { _themeConfig = value; }
        }
    }
}
