using Microsoft.Owin;
using System.Threading.Tasks;

namespace Vin.Core.MultiTenancy
{
    public interface ITenantEngine
    {
        Task BeginRequest(IOwinContext context);
        Task EndRequest(IOwinContext context);
    }
}
