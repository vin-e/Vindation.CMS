using System.Data.Entity.ModelConfiguration;
using Vin.Sample.Data.Model.Marketing;

namespace Vin.Sample.Data.Mapping.Marketing
{
    public class ContactMap : EntityTypeConfiguration<Contact>
    {
        public ContactMap()
        {
            this.ToTable("Contacts");
            this.HasKey(x => x.ID);
            this.HasRequired(x => x.Tenant);
        }
    }
}
