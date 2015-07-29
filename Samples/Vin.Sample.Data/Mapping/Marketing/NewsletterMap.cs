using System.Data.Entity.ModelConfiguration;
using Vin.Sample.Data.Model.Marketing;

namespace Vin.Sample.Data.Mapping.Marketing
{
    public class NewsletterMap : EntityTypeConfiguration<Newsletter>
    {
        public NewsletterMap()
        {
            this.ToTable("Newsletters");
            this.HasKey(x => x.ID);
            this.HasRequired(x => x.Tenant);
        }
    }
}
