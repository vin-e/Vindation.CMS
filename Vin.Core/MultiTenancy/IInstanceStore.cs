using System;

namespace Vin.Core.MultiTenancy
{
    public interface IInstanceStore
    {
        TenantInstance Get(string requestIdentifier);
        void Add(TenantInstance instance, Action<string, TenantInstance> removedCallback);
        void Remove(string instanceId);
    }
}
