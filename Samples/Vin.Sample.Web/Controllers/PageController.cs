using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vin.Core.Filters;
using Vin.Sample.Business.Pages;
using Vin.Sample.Data.Model.Pages;
using Vin.Sample.Web.Models;

namespace Vin.Sample.Web.Controllers
{
    [TenantRequiredActionFilter]
    public class PageController : Controller
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            this._pageService = pageService;
        }

        public ActionResult Index(string id)
        {
            bool isHomePage = string.IsNullOrEmpty(id);
            Page page;
            if (isHomePage)
                page = _pageService.Table.FirstOrDefault(p => p.Meta.IsHomePage == true);
            else
                page = _pageService.Table.FirstOrDefault(p => p.Meta.Permalink == id);

            if (page == null || page.ID <= 0)
            {
                if (isHomePage)
                    return Redirect("~/Error/NotYetConfigured");

                return Redirect("~/Error/404");
            }

            HttpContext.Items["Vin_Meta"] = _pageService.GetMetaData(page);
            return View(new PageViewModel(page));
        }
    }
}