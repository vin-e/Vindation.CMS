using System;

namespace Vin.Core.MultiTenancy
{
    public class TenantConfiguration : ITenantConfiguration
    {
        public RequestIdentificationStrategy IdentificationStrategy { get; set; }

        public ITenantResolver TenantResolver { get; set; }

        public Action<string> Logger { get; set; }

        public TenantConfiguration()
        {
            Logger = log => { };
        }

        public bool IsValid
        {
            get
            {
                return true;
            }
        }
    }
}
