using System.Data.Entity.ModelConfiguration;
using Vin.Sample.Data.Model.Posts;

namespace Vin.Sample.Data.Mapping.Posts
{
    public class PostMap : EntityTypeConfiguration<Post>
    {
        public PostMap()
        {
            this.ToTable("Posts");
            this.HasKey(x => x.ID);

            this.HasRequired(x => x.Tenant);
            this.HasRequired(x => x.Meta);
            this.HasOptional(x => x.HeaderImage);
        }
    }
}
