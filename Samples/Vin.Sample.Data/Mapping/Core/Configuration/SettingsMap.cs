using System.Data.Entity.ModelConfiguration;
using Vin.Core.Model.Configuration;

namespace Vin.Sample.Data.Mapping.Core.Configuration
{
    public class SettingsMap : EntityTypeConfiguration<Setting>
    {
        public SettingsMap()
        {
            this.ToTable("Settings");
            this.HasKey(x => x.ID);
            this.HasRequired(x => x.Tenant);
        }
    }
}
