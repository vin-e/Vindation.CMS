using System.Collections.Generic;
using System.Linq;
using Vin.Core.Web;
using Vin.Sample.Data.Model.Posts;

namespace Vin.Sample.Business.Posts
{
    public partial interface IPostService
    {
        IQueryable<Post> Table { get; }
        void Insert(Post entity);
        void Update(Post entity);
        Post GetByID(int id);

        MetaInfo GetMetaData(Post entity);
    }
}
