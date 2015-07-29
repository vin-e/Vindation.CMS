using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vin.Sample.Business.Marketing;
using Vin.Sample.Web.Areas.Admin.Models;

namespace Vin.Sample.Web.Areas.Admin.Controllers
{
    public class MarketingController : Controller
    {
        private readonly IContactService _contactService;
        private readonly INewsletterService _newsletterService;
        
        public MarketingController(IContactService contactService, INewsletterService newsletterService)
        {
            this._contactService = contactService;
            this._newsletterService = newsletterService;
        }

        // GET: Admin/Marketing
        public ActionResult Newsletter()
        {
            return View();
        }

        public ActionResult Leads()
        {
            return View();
        }

        [HttpPost]
        public JsonResult NewsletterDataTable(int length, int start)
        {
            var e = _newsletterService.Table.AsQueryable();

            var query = HttpContext.Request.Params["search[value]"];
            if (!string.IsNullOrEmpty(query))
                e = e.Where(x => x.Email.Contains(query));

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
                        e = e.OrderByDescending(ob => ob.Email);
                    else
                        e = e.OrderBy(ob => ob.Email);
                    break;
                case 2:
                    if (sortDir == "desc")
                        e = e.OrderByDescending(ob => ob.OptIn);
                    else
                        e = e.OrderBy(ob => ob.OptIn);
                    break;
                default:
                    if (sortDir == "desc")
                        e = e.OrderByDescending(ob => ob.Email);
                    else
                        e = e.OrderBy(ob => ob.Email);
                    break;
            }
            #endregion

            var response = new
            {
                iTotalRecords = e.Count(),
                iTotalDisplayRecords = e.Count(),
                d = e.Select(x => new NewsletterDataViewModel
                {
                    Id = x.ID,
                    Email = x.Email,
                    OptIn = x.OptIn,
                    CreatedDate = SqlFunctions.DatePart("mm", x.CreatedDate) + "/" + SqlFunctions.DatePart("dd", x.CreatedDate) + "/" + SqlFunctions.DatePart("yyyy", x.CreatedDate)
                })
                    .Skip(start)
                    .Take(length)
                    .ToList()
            };

            return Json(response);
        }

        [HttpPost]
        public JsonResult LeadsDataTable(int length, int start)
        {
            var e = _contactService.Table.AsQueryable();

            var query = HttpContext.Request.Params["search[value]"];
            if (!string.IsNullOrEmpty(query))
                e = e.Where(x => x.Name.Contains(query) || x.Email.Contains(query) || x.Message.Contains(query) || x.Phone.Contains(query));

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
                case 2:
                    if (sortDir == "desc")
                        e = e.OrderByDescending(ob => ob.Email);
                    else
                        e = e.OrderBy(ob => ob.Email);
                    break;
                case 3:
                    if (sortDir == "desc")
                        e = e.OrderByDescending(ob => ob.Phone);
                    else
                        e = e.OrderBy(ob => ob.Phone);
                    break;
                case 5:
                    if (sortDir == "desc")
                        e = e.OrderByDescending(ob => ob.CreatedDate);
                    else
                        e = e.OrderBy(ob => ob.CreatedDate);
                    break;
                default:
                    if (sortDir == "desc")
                        e = e.OrderByDescending(ob => ob.Name);
                    else
                        e = e.OrderBy(ob => ob.Name);
                    break;
            }
            #endregion

            var response = new
            {
                iTotalRecords = e.Count(),
                iTotalDisplayRecords = e.Count(),
                d = e.Select(x => new LeadsDataViewModel
                {
                    Id = x.ID,
                    Name = x.Name,
                    Email = x.Email,
                    Phone = x.Phone,
                    Message = x.Message,
                    CreatedDate = SqlFunctions.DatePart("mm", x.CreatedDate) + "/" + SqlFunctions.DatePart("dd", x.CreatedDate) + "/" + SqlFunctions.DatePart("yyyy", x.CreatedDate)
                })
                    .Skip(start)
                    .Take(length)
                    .ToList()
            };

            return Json(response);
        }
    }
}