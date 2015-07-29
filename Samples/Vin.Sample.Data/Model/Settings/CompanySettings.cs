using Vin.Core.Configuration;

namespace Vin.Sample.Data.Model.Settings
{
    public class CompanySettings : ISettings
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Ein { get; set; }

        public string Brief { get; set; }
    }
}
