using System.Data.Entity.ModelConfiguration;
using Vin.Sample.Data.Model.Posts;

namespace Vin.Sample.Data.Mapping.Posts
{
    public class TagMap : EntityTypeConfiguration<Tag>
    {
        public TagMap()
        {
            this.ToTable("Tags");
            this.HasKey(x => x.ID);
            this.HasRequired(x => x.Tenant);

            this.HasMany(x => x.Posts)
                .WithMany(x => x.Tags)
                .Map(x =>
                {
                    x.ToTable("Post_Tag_Mapping");
                    x.MapLeftKey("Tag_ID");
                    x.MapRightKey("Post_ID");
                });
        }
    }
}
