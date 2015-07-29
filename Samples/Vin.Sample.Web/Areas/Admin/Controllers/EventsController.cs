using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vin.Sample.Business.Events;
using Vin.Sample.Data.Model.Events;
using Vin.Sample.Web.Areas.Admin.Models;

namespace Vin.Sample.Web.Areas.Admin.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            this._eventService = eventService;
        }

        // GET: Admin/Events
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DataTable(int length, int start)
        {
            var e = _eventService.Table.AsQueryable();

            var query = HttpContext.Request.Params["search[value]"];
            if (!string.IsNullOrEmpty(query))
                e = e.Where(x => x.Name.Contains(query) || x.Details.Contains(query));

            #region Sort Column and Direction
            /*  
                Sort column is (zero) index based and determined on the front end. Please reference the javascript if incorrect columns are being sorted.
             */
            var sortColumn = Convert.ToInt32(HttpContext.Request.Params["order[0][column]"]);
            var sortDir = HttpContext.Request.Params["order[0][dir]"];

            switch (sortColumn)
            {
                case 0:
                    if (sortDir == "desc")
                        e = e.OrderByDescending(ob => ob.ID);
                    else
                        e = e.OrderBy(ob => ob.ID);
                    break;
                case 1:
                    if (sortDir == "desc")
                        e = e.OrderByDescending(ob => ob.Name);
                    else
                        e = e.OrderBy(ob => ob.Name);
                    break;
                case 3:
                    if (sortDir == "desc")
                        e = e.OrderByDescending(ob => ob.EndDate);
                    else
                        e = e.OrderBy(ob => ob.EndDate);
                    break;
                case 2:
                default:
                    if (sortDir == "desc")
                        e = e.OrderByDescending(ob => ob.StartDate);
                    else
                        e = e.OrderBy(ob => ob.StartDate);
                    break;
            }
            #endregion

            var response = new
                {
                    iTotalRecords = e.Count(),
                    iTotalDisplayRecords = e.Count(),
                    d = e.Select(x => new EventDataViewModel
                        {
                            Id = x.ID,
                            Name = x.Name,
                            EndDate = SqlFunctions.DatePart("mm", x.EndDate) + "/" + SqlFunctions.DatePart("dd", x.EndDate) + "/" + SqlFunctions.DatePart("yyyy", x.EndDate),
                            StartDate = SqlFunctions.DatePart("mm", x.StartDate) + "/" + SqlFunctions.DatePart("dd", x.StartDate) + "/" + SqlFunctions.DatePart("yyyy", x.StartDate),
                            IsPublished = x.IsPublished
                        })
                        .Skip(start)
                        .Take(length)
                        .ToList()
                };

            

            return Json(response);
        }

        public ActionResult Details(int id)
        {
            var e = _eventService.GetByID(id);
            var eViewModel = new EventDataViewModel();

            if (e != null && e.ID > 0)
                eViewModel = new EventDataViewModel()
                {
                    Id = e.ID,
                    Name = e.Name,
                    IsPublished = e.IsPublished,
                    StartDate = e.StartDate.ToString(),
                    EndDate = e.EndDate.ToString(),
                    Details = e.Details,
                    Address = e.Address,
                    City = e.City,
                    State = e.State,
                    ZipCode = e.ZipCode,
                    CreatedDate = e.CreatedDate,
                    UpdatedDate = e.UpdatedDate
                };

            return View(eViewModel);
        }

        [HttpPost]
        public ActionResult Details(EventDataViewModel e)
        {
            if (string.IsNullOrEmpty(e.Name) || string.IsNullOrEmpty(e.Details))
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Missing required data.");

            Mapper.CreateMap<EventDataViewModel, Event>();            
            var evt = Mapper.Map(e, _eventService.GetByID(e.Id));
            evt.UpdatedDate = DateTime.UtcNow;

            _eventService.Update(evt);

            return View(e);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EventDataViewModel e)
        {
            e.CreatedDate = DateTime.UtcNow;
            e.UpdatedDate = DateTime.UtcNow;

            Mapper.CreateMap<EventDataViewModel, Event>();
            var evt = Mapper.Map<EventDataViewModel, Event>(e);

            _eventService.Insert(evt);

            return RedirectToAction("Details", new { id = evt.ID });
        }

        [HttpPost]
        public bool Publish(int id, bool isPublished)
        {
            var e = _eventService.GetByID(id);

            if (e == null || e.ID <= 0)
                return false;

            e.IsPublished = isPublished;
            _eventService.Update(e);

            return true;
        }
    }
}