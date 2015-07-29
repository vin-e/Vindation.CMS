using System.Linq;
using Vin.Sample.Data.Model.Marketing;

namespace Vin.Sample.Business.Marketing
{
    public partial interface INewsletterService
    {
        IQueryable<Newsletter> Table { get; }
        void Insert(Newsletter entity);
        void Update(Newsletter entity);
        Newsletter GetByID(int id);
    }
}
