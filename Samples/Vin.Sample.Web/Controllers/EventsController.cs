using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vin.Core.Filters;
using Vin.Core.Web;
using Vin.Sample.Business.Events;
using Vin.Sample.Web.Models;

namespace Vin.Sample.Web.Controllers
{
    [TenantRequiredActionFilter]
    [RoutePrefix("events")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            this._eventService = eventService;
        }

        [Route]
        public ActionResult Index(int page = 1)
        {
            var take = 10;
            var skip = (page - 1) * take;

            var eventsViewModel = new EventsViewModel()
                {
                    Events = new List<EventViewModel>()
                };

            var startDate = DateTime.Now.AddDays(-7);
            var events = _eventService.Table.Where(e => e.IsPublished == true && e.StartDate >= startDate);
            eventsViewModel.CurrentPage = page;
            eventsViewModel.TotalEvents = events.Count();
            eventsViewModel.TotalPages = (int)Math.Ceiling(eventsViewModel.TotalEvents * 1.0 / 10);

            foreach (var e in events.OrderBy(ob => ob.StartDate).Skip(skip).Take(take))
                eventsViewModel.Events.Add(new EventViewModel()
                    {
                        Id = e.ID,
                        Name = e.Name,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        Details = e.Details,
                        Address = e.Address,
                        City = e.City,
                        State = e.State,
                        ZipCode = e.ZipCode
                    });

            HttpContext.Items["Vin_Meta"] = new MetaInfo()
            {
                MetaDescription = "List of past and upcoming events",
                MetaKeywords = "Events",
                MetaTitle = "Events",
                PermalinkName = "/Events"
            };

            return View(eventsViewModel);
        }

        [Route("{id}")]
        public ActionResult SingleEvent(int id)
        {
            if (id <= 0)
                return RedirectToRoute("~/");

            var e = _eventService.GetByID(id);

            if (e == null || e.ID <= 0)
                return Redirect("~/Error/404");

            HttpContext.Items["Vin_Meta"] = _eventService.GetMetaData(e);

            return View(new EventViewModel(e));
        }
    }
}