using System.Data.Entity.ModelConfiguration;
using Vin.Core.Model.Locations;

namespace Vin.Sample.Data.Mapping.Core.Locations
{
    public class StateMap : EntityTypeConfiguration<State>
    {
        public StateMap()
        {
            this.ToTable("States");
            this.HasKey(x => x.ID);
        }
    }
}
