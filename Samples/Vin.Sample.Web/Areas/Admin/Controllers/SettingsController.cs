using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vin.Core.Model.MultiTenancy;
using Vin.Core.MultiTenancy;
using Vin.Core.Services.Configuration;
using Vin.Core.Services.Themes;
using Vin.Sample.Data.Model.Settings;
using Vin.Sample.Web.Areas.Admin.Models;

namespace Vin.Sample.Web.Areas.Admin.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IThemeService _themeService;

        public SettingsController(ISettingService settingService, IThemeService themeService)
        {
            this._settingService = settingService;
            this._themeService = themeService;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/CallToAction
        public ActionResult CallToAction()
        {
            return View(_settingService.LoadSetting<CallToActionSettings>());
        }

        [HttpPost]
        public ActionResult CallToAction(CallToActionSettings settings)
        {
            _settingService.SaveSetting<CallToActionSettings>(settings);

            return View(_settingService.LoadSetting<CallToActionSettings>());
        }

        // GET: Admin/CallToAction
        public ActionResult Company()
        {
            return View(_settingService.LoadSetting<CompanySettings>());
        }

        [HttpPost]
        public ActionResult Company(CompanySettings settings)
        {
            _settingService.SaveSetting<CompanySettings>(settings);

            return View(_settingService.LoadSetting<CompanySettings>());
        }

        // GET: Admin/CallToAction
        public ActionResult Site()
        {
            var settings = new SiteSettingsViewModel();

            settings.SiteSettings = _settingService.LoadSetting<SiteSettings>();
            settings.TenantSettings = _settingService.LoadSetting<TenantSettings>();

            return View(settings);
        }

        [HttpPost]
        public ActionResult Site(SiteSettingsViewModel settings)
        {
            _settingService.SaveSetting<SiteSettings>(settings.SiteSettings);
            _settingService.SaveSetting<TenantSettings>(settings.TenantSettings);

            return View(settings);
        }

        // GET: Admin/CallToAction
        public ActionResult Social()
        {
            return View(_settingService.LoadSetting<SocialSettings>());
        }

        [HttpPost]
        public ActionResult Social(SocialSettings settings)
        {
            _settingService.SaveSetting<SocialSettings>(settings);

            return View(_settingService.LoadSetting<SocialSettings>());
        }

        public ActionResult Theme()
        {
            var context = HttpContext.GetOwinContext();
            var tenantInstance = context.GetTenantInstance();

            var viewModel = new ThemeSettingsViewModel();
            viewModel.CurrentTheme = tenantInstance.Tenant.ThemeConfig;

            viewModel.ThemeConfigs = _themeService.GetThemeConfigurations().Where(x => x.ThemeName != viewModel.CurrentTheme.ThemeName).ToList();

            return View(viewModel);
        }

        public ActionResult ApplyTheme(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var context = HttpContext.GetOwinContext();
                var tenantInstance = context.GetTenantInstance();
                if (tenantInstance.Tenant.ThemeConfig.ThemeName != id)
                {
                    var theme = _themeService.GetThemeConfigurations().Where(x => x.ThemeName == id).FirstOrDefault();

                    if (theme != null && !string.IsNullOrEmpty(theme.ThemeName))
                    {
                        var tenantSettings = _settingService.LoadSetting<TenantSettings>();
                        tenantSettings.DefaultTheme = id;
                        _settingService.SaveSetting<TenantSettings>(tenantSettings);

                        tenantInstance.Tenant.ThemeConfig = theme;
                    }
                }
            }
            
            return RedirectToAction("Theme");
        }
    }
}