using System.Data.Entity.ModelConfiguration;
using Vin.Sample.Data.Model.Medias;

namespace Vin.Sample.Data.Mapping.Medias
{
    public class MediaMap : EntityTypeConfiguration<Media>
    {
        public MediaMap()
        {
            this.ToTable("MediaFiles");
            this.HasKey(x => x.ID);
            this.HasRequired(x => x.Tenant);
        }
    }
}
