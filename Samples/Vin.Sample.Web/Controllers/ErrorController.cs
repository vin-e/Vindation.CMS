using System.Web.Mvc;

namespace Vin.Sample.Web.Controllers
{
    [RoutePrefix("error")]
    public class ErrorController : Controller
    {
        [Route("{id?}")]
        public ActionResult Index(int? id)
        {
            return View(id);
        }

        [Route("NotYetConfigured")]
        public ActionResult NotYetConfigured()
        {
            return View();
        }

        [Route("UnknownTenant")]
        public ActionResult UnknownTenant()
        {
            return View();
        }

        [Route("Maintenance")]
        public ActionResult Maintenance()
        {
            return View();
        }
    }
}