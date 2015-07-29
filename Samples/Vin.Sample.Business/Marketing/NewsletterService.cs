using System.Linq;
using Vin.Core.Data.Repositories;
using Vin.Sample.Data.Model.Marketing;

namespace Vin.Sample.Business.Marketing
{
    public class NewsletterService : INewsletterService
    {
        private readonly IRepository<Newsletter> _newsletterRepository;

        public NewsletterService(IRepository<Newsletter> newsletterRepository)
        {
            this._newsletterRepository = newsletterRepository;
        }

        public IQueryable<Newsletter> Table
        {
            get { return _newsletterRepository.Table; }
        }

        public void Insert(Newsletter entity)
        {
            _newsletterRepository.Insert(entity);
        }

        public void Update(Newsletter entity)
        {
            _newsletterRepository.Update(entity);
        }

        public Newsletter GetByID(int id)
        {
            return _newsletterRepository.GetById(id);
        }
    }
}
