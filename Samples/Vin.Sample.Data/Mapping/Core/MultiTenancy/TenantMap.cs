using System.Data.Entity.ModelConfiguration;
using Vin.Core.Model.MultiTenancy;

namespace Vin.Sample.Data.Mapping.Core.MultiTenancy
{
    public class TenantMap : EntityTypeConfiguration<Tenant>
    {
        public TenantMap()
        {
            this.ToTable("Tenants");
            this.HasKey(t => t.ID);
        }
    }
}
