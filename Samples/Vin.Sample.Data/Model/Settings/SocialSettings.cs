using Vin.Core.Configuration;

namespace Vin.Sample.Data.Model.Settings
{
    public class SocialSettings : ISettings
    {
        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string GooglePlus { get; set; }

        public string YouTube { get; set; }

        public string LinkedIn { get; set; }

        public string Pinterest { get; set; }
    }
}
