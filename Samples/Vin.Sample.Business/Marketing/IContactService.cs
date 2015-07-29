using System.Linq;
using Vin.Sample.Data.Model.Marketing;

namespace Vin.Sample.Business.Marketing
{
    public partial interface IContactService
    {
        IQueryable<Contact> Table { get; }
        void Insert(Contact entity);
        void Update(Contact entity);
        Contact GetByID(int id);
    }
}
