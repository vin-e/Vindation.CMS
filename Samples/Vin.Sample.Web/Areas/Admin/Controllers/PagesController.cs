using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vin.Sample.Business.Pages;
using Vin.Sample.Data.Model.Pages;
using Vin.Sample.Web.Areas.Admin.Models;

namespace Vin.Sample.Web.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService)
        {
            this._pageService = pageService;
        }

        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var page = _pageService.GetByID(id);
            var pageViewModel = new PageDataViewModel()
                {
                    Id = page.ID,
                    Title = page.Title,
                    HtmlBody = page.HtmlBody,
                    TextBody = page.TextBody,
                    IsPublished = page.IsPublished,
                    CreatedDate = page.CreatedDate.ToString("f"),
                    UpdatedDate = page.UpdatedDate.ToString("f")
                };

            return View(pageViewModel);
        }

        [HttpPost]
        public ActionResult Details(PageDataViewModel page)
        {
            if (string.IsNullOrEmpty(page.TextBody) || string.IsNullOrEmpty(page.HtmlBody))
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Missing required data.");

            Mapper.CreateMap<PageDataViewModel, Page>();
            var updatedPage = Mapper.Map(page, _pageService.GetByID(page.Id));
            updatedPage.UpdatedDate = DateTime.UtcNow;

            _pageService.Update(updatedPage);

            return View(page);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PageDataViewModel page)
        {
            if (string.IsNullOrEmpty(page.TextBody) || string.IsNullOrEmpty(page.HtmlBody))
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Missing required data.");

            Mapper.CreateMap<PageDataViewModel, Page>();
            var newPage = Mapper.Map<PageDataViewModel, Page>(page);
            newPage.CreatedDate = DateTime.UtcNow;
            newPage.UpdatedDate = DateTime.UtcNow;

            _pageService.Insert(newPage);

            return RedirectToAction("Details", new { id = newPage.ID }); ;
        }

        [HttpPost]
        public JsonResult DataTable(int length, int start)
        {
            var e = _pageService.Table.AsQueryable();

            var query = HttpContext.Request.Params["search[value]"];
            if (!string.IsNullOrEmpty(query))
                e = e.Where(x => x.Title.Contains(query) || x.TextBody.Contains(query));

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
                        e = e.OrderByDescending(ob => ob.Title);
                    else
                        e = e.OrderBy(ob => ob.Title);
                    break;
                case 2:
                    if (sortDir == "desc")
                        e = e.OrderByDescending(ob => ob.IsPublished);
                    else
                        e = e.OrderBy(ob => ob.IsPublished);
                    break;
                case 3:
                default:
                    if (sortDir == "desc")
                        e = e.OrderByDescending(ob => ob.UpdatedDate);
                    else
                        e = e.OrderBy(ob => ob.UpdatedDate);
                    break;
            }
            #endregion

            var response = new
            {
                iTotalRecords = e.Count(),
                iTotalDisplayRecords = e.Count(),
                d = e.Select(x => new PageDataViewModel
                {
                    Id = x.ID,
                    Title = x.Title,
                    TextBody = x.TextBody,
                    UpdatedDate = SqlFunctions.DatePart("mm", x.UpdatedDate) + "/" + SqlFunctions.DatePart("dd", x.UpdatedDate) + "/" + SqlFunctions.DatePart("yyyy", x.UpdatedDate),
                    IsPublished = x.IsPublished
                })
                    .Skip(start)
                    .Take(length)
                    .ToList()
            };

            return Json(response);
        }
    }
}