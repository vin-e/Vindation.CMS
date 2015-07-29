using System.Web.Mvc;
using Vin.Core.Filters;
using Vin.Core.Services.Configuration;
using Vin.Sample.Business.Marketing;
using Vin.Sample.Data.Model.Marketing;
using Vin.Sample.Data.Model.Settings;

namespace Vin.Sample.Web.Controllers
{
    [TenantRequiredActionFilter]
    [RoutePrefix("contact")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ISettingService _settingService;

        public ContactController(IContactService contactService, ISettingService settingService)
        {
            this._contactService = contactService;
            this._settingService = settingService;
        }

        [Route]
        public ActionResult Index()
        {
            var companySettings = _settingService.LoadSetting<CompanySettings>();

            return View(companySettings);
        }

        [Route, HttpPost]
        public ActionResult Index(string name, string email, string phone, string message)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                var companySettings = _settingService.LoadSetting<CompanySettings>();
                return View(companySettings);
            }

            _contactService.Insert(new Contact() { Name = name, Email = email, Phone = phone, Message = message });

            return RedirectToAction("ThankYou");
        }

        [Route("ThankYou")]
        public ActionResult ThankYou()
        {
            var companySettings = _settingService.LoadSetting<CompanySettings>();
            return View(companySettings);
        }
	}
}