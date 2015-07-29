using System.Linq;
using Vin.Core.Data.Repositories;
using Vin.Sample.Data.Model.Marketing;

namespace Vin.Sample.Business.Marketing
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> _contactRepository;

        public ContactService(IRepository<Contact> contactRepository)
        {
            this._contactRepository = contactRepository;
        }

        public IQueryable<Contact> Table
        {
            get { return _contactRepository.Table; }
        }

        public void Insert(Contact entity)
        {
            _contactRepository.Insert(entity);
        }

        public void Update(Contact entity)
        {
            _contactRepository.Update(entity);
        }

        public Contact GetByID(int id)
        {
            return _contactRepository.GetById(id);
        }
    }
}
