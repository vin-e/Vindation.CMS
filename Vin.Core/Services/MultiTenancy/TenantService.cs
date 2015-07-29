using System.Linq;
using Vin.Core.Data.Repositories;
using Vin.Core.Model.MultiTenancy;

namespace Vin.Core.Services.MultiTenancy
{
    public partial class TenantService : ITenantService
    {
        private readonly IRepository<Tenant> _tenantRepository;

        public TenantService(IRepository<Tenant> tenantRepository)
        {
            this._tenantRepository = tenantRepository;
        }

        public virtual IQueryable<Tenant> Table
        {
            get { return _tenantRepository.Table; }
        }

        public virtual void Insert(Tenant entity)
        {
            _tenantRepository.Insert(entity);
        }

        public virtual void Update(Tenant entity)
        {
            _tenantRepository.Update(entity);
        }

        public virtual Tenant GetByID(int id)
        {
            return _tenantRepository.GetById(id);
        }

        public virtual Tenant GetByIdentifier(string identifier)
        {
            return this.Table.FirstOrDefault(t => t.RequestIdentifiers.Any(ti => ti.Name == identifier));
        }
    }
}
