using System.Threading.Tasks;
using Vin.Core.Model.MultiTenancy;

namespace Vin.Core.MultiTenancy
{
    public interface ITenantResolver
    {
        Task<Tenant> Resolve(string requestIdentifier);
    }
}
