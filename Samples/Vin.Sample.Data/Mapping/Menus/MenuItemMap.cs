using System.Data.Entity.ModelConfiguration;
using Vin.Sample.Data.Model.Menus;

namespace Vin.Sample.Data.Mapping.Menus
{
    public class MenuItemMap : EntityTypeConfiguration<MenuItem>
    {
        public MenuItemMap()
        {
            this.ToTable("MenuItems");
            this.HasKey(x => x.ID);

            this.HasRequired(x => x.Information);
            this.HasOptional(x => x.ParentMenu);

            this.HasRequired(x => x.Menu)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.Menu_ID);
        }
    }
}
