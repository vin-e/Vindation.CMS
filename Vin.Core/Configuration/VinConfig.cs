using System.Configuration;
using System.Xml;

namespace Vin.Core.Configuration
{
    public class Config : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new Config();

            var themeNode = section.SelectSingleNode("Themes");
            if (themeNode != null && themeNode.Attributes != null)
            {
                var basePathAttribute = themeNode.Attributes["basePath"];
                if (basePathAttribute != null)
                    config.ThemeBasePath = basePathAttribute.Value;

                var defaultThemeAttribute = themeNode.Attributes["defaultTheme"];
                if (defaultThemeAttribute != null)
                    config.DefaultTheme = defaultThemeAttribute.Value;
            }

            return config;
        }

        public string DefaultTheme { get; private set; }

        public string ThemeBasePath { get; private set; }
    }
}
