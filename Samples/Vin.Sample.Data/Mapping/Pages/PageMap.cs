using System.Data.Entity.ModelConfiguration;
using Vin.Sample.Data.Model.Pages;

namespace Vin.Sample.Data.Mapping.Pages
{
    public class PageMap  : EntityTypeConfiguration<Page>
    {
        public PageMap()
        {
            this.ToTable("Pages");
            this.HasKey(x => x.ID);

            this.HasRequired(x => x.Tenant);
            this.HasRequired(x => x.Meta);
            this.HasOptional(x => x.ParentPage);
            this.HasOptional(x => x.HeaderImage);
        }
    }
}
