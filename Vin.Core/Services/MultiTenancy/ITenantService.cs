using System.Linq;
using Vin.Core.Model.MultiTenancy;

namespace Vin.Core.Services.MultiTenancy
{
    public interface ITenantService
    {
        IQueryable<Tenant> Table { get; }
        void Insert(Tenant entity);
        void Update(Tenant entity);
        Tenant GetByID(int id);
        Tenant GetByIdentifier(string identifier);
    }
}
