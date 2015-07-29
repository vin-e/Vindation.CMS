using System.Collections.Generic;

namespace Vin.Core.Model.Themes
{
    /// <summary>
    /// Theme Configuration
    /// </summary>
    public class ThemeConfiguration
    {
        /// <summary>
        /// Gets or sets the absolute path for the files
        /// </summary>
        public string AbsolutePath { get; set; }

        /// <summary>
        /// Gets or sets the relative path for the files
        /// </summary>
        public string RelativePath { get; set; }

        /// <summary>
        /// Gets or sets the url for the theme preview image
        /// </summary>
        public string PreviewImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the text defining the theme
        /// </summary>
        public string PreviewText { get; set; }

        /// <summary>
        /// Gets or sets the theme name
        /// </summary>
        public string ThemeName { get; set; }

        /// <summary>
        /// Gets or sets the theme title
        /// </summary>
        public string ThemeTitle { get; set; }

        /// <summary>
        /// Gets or sets a collection of css files
        /// </summary>
        public List<string> Css { get; set; }

        /// <summary>
        /// Gets or sets a collection javascript files
        /// </summary>
        public List<string> Javascript { get; set; }

        /// <summary>
        /// Gets or sets the setting if the theme is available to only one client
        /// </summary>
        public List<string> AvailableToTenants { get; set; }
    }
}
