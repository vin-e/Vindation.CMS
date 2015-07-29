using System.Data.Entity.ModelConfiguration;
using Vin.Sample.Data.Model.Menus;

namespace Vin.Sample.Data.Mapping.Menus
{
    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            this.ToTable("Menus");
            this.HasKey(x => x.ID);
            this.HasRequired(x => x.Tenant);

        }
    }
}
