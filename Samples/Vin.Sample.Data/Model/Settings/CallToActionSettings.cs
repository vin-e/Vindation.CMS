using Vin.Core.Configuration;

namespace Vin.Sample.Data.Model.Settings
{
    public class CallToActionSettings : ISettings
    {
        public string LinkOneText { get; set; }

        public string LinkOneUrl { get; set; }

        public string LinkTwoText { get; set; }

        public string LinkTwoUrl { get; set; }
    }
}
