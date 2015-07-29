using Vin.Core.Configuration;

namespace Vin.Sample.Data.Model.Settings
{
    public class SiteSettings : ISettings
    {
        public bool EnableNewsletter { get; set; }

        public bool EnableTwitterFeed { get; set; }

        public bool EnableGoogleAnalytics { get; set; }

        public string GoogleAnaltyicsId { get; set; }
    }
}
