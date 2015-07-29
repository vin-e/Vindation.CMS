using System.Collections.Generic;
using System.Linq;
using Vin.Core.Data.Repositories;
using Vin.Core.Web;
using Vin.Sample.Data.Model.Posts;

namespace Vin.Sample.Business.Posts
{
    public partial class PostService : IPostService
    {
        private readonly IRepository<Post> _postRepository;

        public PostService(IRepository<Post> postRepository)
        {
            this._postRepository = postRepository;
        }

        public virtual IQueryable<Post> Table
        {
            get { return _postRepository.Table; }
        }

        public virtual void Insert(Post entity)
        {
            _postRepository.Insert(entity);
        }

        public virtual void Update(Post entity)
        {
            _postRepository.Update(entity);
        }

        public virtual Post GetByID(int id)
        {
            return _postRepository.GetById(id);
        }

        public virtual MetaInfo GetMetaData(Post entity)
        {
            return new MetaInfo() { MetaDescription = entity.Meta.MetaDescription, MetaKeywords = entity.Meta.MetaKeywords, MetaTitle = entity.Title, PermalinkName = entity.Meta.Permalink };
        }
    }
}
