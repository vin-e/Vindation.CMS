using System.Linq;
using Vin.Core.Data.Repositories;
using Vin.Core.Web;
using Vin.Sample.Data.Model.Events;

namespace Vin.Sample.Business.Events
{
    public partial class EventService : IEventService
    {
        private readonly IRepository<Event> _eventService;

        public EventService(IRepository<Event> eventService)
        {
            this._eventService = eventService;
        }

        public IQueryable<Event> Table
        {
            get { return _eventService.Table; }
        }

        public void Insert(Event entity)
        {
            _eventService.Insert(entity);
        }

        public void Update(Event entity)
        {
            _eventService.Update(entity);
        }

        public Event GetByID(int id)
        {
            return _eventService.GetById(id);
        }

        public virtual MetaInfo GetMetaData(Event entity)
        {
            return new MetaInfo()
            {
                MetaDescription = string.Format("{0} - {1}", entity.Name, entity.StartDate),
                MetaKeywords = entity.Name,
                MetaTitle = entity.Name,
                PermalinkName = string.Format("/events/{0}", entity.ID)
            };
        }
    }
}
