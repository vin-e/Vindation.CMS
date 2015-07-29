using System.Collections.Generic;
using Vin.Core.Model.MultiTenancy;
using Vin.Core.Model.Themes;

namespace Vin.Core.Services.Themes
{
    public interface IThemeService
    {
        ThemeConfiguration GetThemeConfiguration(string themeName);

        ThemeConfiguration GetTenantThemeConfiguration(Tenant tenant);
        
        IList<ThemeConfiguration> GetThemeConfigurations();
        
        bool ThemeConfigurationExists(string themeName);
    }
}
