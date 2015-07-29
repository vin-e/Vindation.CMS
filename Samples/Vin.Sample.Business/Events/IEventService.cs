using System.Linq;
using Vin.Core.Web;
using Vin.Sample.Data.Model.Events;

namespace Vin.Sample.Business.Events
{
    public partial interface IEventService
    {
        IQueryable<Event> Table { get; }
        void Insert(Event entity);
        void Update(Event entity);
        Event GetByID(int id);
        MetaInfo GetMetaData(Event entity);
    }
}
