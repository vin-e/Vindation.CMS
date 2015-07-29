using System.Data.Entity.ModelConfiguration;
using Vin.Core.Model.MultiTenancy;

namespace Vin.Sample.Data.Mapping.Core.MultiTenancy
{
    public class TenantIdentifierMap: EntityTypeConfiguration<TenantIdentifier>
    {
        public TenantIdentifierMap()
        {
            this.ToTable("TenantIdentifiers");
            this.HasKey(t => t.ID);
            this.HasRequired(x => x.Tenant);
        }
    }
}
