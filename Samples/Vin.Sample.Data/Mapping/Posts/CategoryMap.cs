using System.Data.Entity.ModelConfiguration;
using Vin.Sample.Data.Model.Posts;

namespace Vin.Sample.Data.Mapping.Posts
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            this.ToTable("Categories");
            this.HasKey(x => x.ID);
            this.HasRequired(x => x.Tenant);

            this.HasMany(x => x.Posts)
                .WithMany(x => x.Categories)
                .Map(x =>
                {
                    x.ToTable("Post_Category_Mapping");
                    x.MapLeftKey("Category_ID");
                    x.MapRightKey("Post_ID");
                });
        }
    }
}
