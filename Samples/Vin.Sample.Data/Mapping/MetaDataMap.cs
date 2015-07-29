using System.Data.Entity.ModelConfiguration;
using Vin.Sample.Data.Model;

namespace Vin.Sample.Data.Mapping
{
    public class MetaDataMap : EntityTypeConfiguration<MetaData>
    {
        public MetaDataMap()
        {
            this.ToTable("MetaData");
            this.HasKey(x => x.ID);
            this.HasRequired(x => x.Tenant);
        }
    }
}
