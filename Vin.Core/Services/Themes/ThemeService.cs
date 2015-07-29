using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Vin.Core.Configuration;
using Vin.Core.Model.MultiTenancy;
using Vin.Core.Model.Themes;
using Vin.Core.Services.Configuration;
using Vin.Core.Utilities;

namespace Vin.Core.Services.Themes
{
    public partial class ThemeService : IThemeService
    {
        #region Fields

        private readonly IList<ThemeConfiguration> _themeConfigs = new List<ThemeConfiguration>();
        private string basePath = string.Empty;
        private string relativePath = string.Empty;
        private string defaultTheme = string.Empty;
        private readonly ISettingService _settingService;

        #endregion

		#region Constructors

        public ThemeService(ISettingService settingService)
        {
            this._settingService = settingService;
            var config = ConfigurationManager.GetSection("VinConfig") as Config;
            relativePath = config.ThemeBasePath;
            basePath = WebUtiliities.MapPath(config.ThemeBasePath);
            defaultTheme = config.DefaultTheme;
            
            LoadConfigurations();
        }
        
		#endregion 
        
        #region IThemeProvider

        public ThemeConfiguration GetThemeConfiguration(string themeName)
        {
            return _themeConfigs
                .SingleOrDefault(x => x.ThemeName.Equals(themeName, StringComparison.InvariantCultureIgnoreCase));
        }

        public ThemeConfiguration GetTenantThemeConfiguration(Tenant tenant)
        {
            ThemeConfiguration config = _themeConfigs.SingleOrDefault(x => x.ThemeName.Equals(defaultTheme, StringComparison.InvariantCultureIgnoreCase));
            
            var _tenantSetting = _settingService.LoadSetting<TenantSettings>();
            if (!string.IsNullOrEmpty(_tenantSetting.DefaultTheme))
            {
                var tenantID = tenant.ID.ToString();
                var themeName = _tenantSetting.DefaultTheme;
                var themeConfig = _themeConfigs
                        .SingleOrDefault(x =>
                            (x.AvailableToTenants.Any(a => a == "*") || x.AvailableToTenants.Any(a => a == tenantID))
                            && x.ThemeName.Equals(themeName, StringComparison.InvariantCultureIgnoreCase));

                if (themeConfig != null && !string.IsNullOrEmpty(themeConfig.ThemeName))
                    config = themeConfig;
            }
            
            return config;
        }

        public IList<ThemeConfiguration> GetThemeConfigurations()
        {
            return _themeConfigs;
        }

        public bool ThemeConfigurationExists(string themeName)
        {
            return _themeConfigs.Any(configuration => configuration.ThemeName.Equals(themeName, StringComparison.InvariantCultureIgnoreCase));
        }

        #endregion

        #region Utility

        private void LoadConfigurations()
        {
            foreach (string themeName in Directory.GetDirectories(basePath))
            {
                var configuration = CreateThemeConfiguration(themeName);
                if (configuration != null)
                {
                    _themeConfigs.Add(configuration);
                }
            }
        }

        private ThemeConfiguration CreateThemeConfiguration(string themePath)
        {
            var themeDirectory = new DirectoryInfo(themePath);
            var themeFilePath = Path.Combine(themeDirectory.FullName, "theme.json");
            var themeConfigFile = new FileInfo(themeFilePath);

            if (themeConfigFile.Exists)
            {
                var config = File.ReadAllText(themeFilePath);
                var theme = JsonConvert.DeserializeObject<ThemeConfiguration>(config);
                theme.RelativePath = relativePath + themeDirectory.Name;
                theme.AbsolutePath = themeDirectory.FullName;
                return theme;
            }

            return null;
        }

        #endregion
    }
}
