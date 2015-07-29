using System.Linq;
using Vin.Core.Data.Repositories;
using Vin.Core.Web;
using Vin.Sample.Data.Model.Pages;

namespace Vin.Sample.Business.Pages
{
    public partial class PageService : IPageService
    {
        private readonly IRepository<Page> _pageRepository;

        public PageService(IRepository<Page> pageRepository)
        {
            this._pageRepository = pageRepository;
        }

        public IQueryable<Page> Table
        {
            get { return _pageRepository.Table; }
        }

        public void Insert(Page entity)
        {
            _pageRepository.Insert(entity);
        }

        public void Update(Page entity)
        {
            _pageRepository.Update(entity);
        }

        public Page GetByID(int id)
        {
            return _pageRepository.GetById(id);
        }

        public MetaInfo GetMetaData(Page entity)
        {
            return new MetaInfo() { MetaDescription = entity.Meta.MetaDescription, MetaKeywords = entity.Meta.MetaKeywords, MetaTitle = entity.Title, PermalinkName = entity.Meta.Permalink };
        }
    }
}
