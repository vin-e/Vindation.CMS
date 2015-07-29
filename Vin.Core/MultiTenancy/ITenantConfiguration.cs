using System;

namespace Vin.Core.MultiTenancy
{
    public interface ITenantConfiguration
    {
        RequestIdentificationStrategy IdentificationStrategy { get; set; }

        ITenantResolver TenantResolver { get; set; }

        Action<string> Logger { get; set; }
    }
}
