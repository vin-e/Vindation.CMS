using System.Data.Entity.ModelConfiguration;
using Vin.Sample.Data.Model.Events;

namespace Vin.Sample.Data.Mapping.Events
{
    public class EventMap : EntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            this.ToTable("Events");
            this.HasKey(x => x.ID);
            this.HasRequired(x => x.Tenant);
        }
    }
}
