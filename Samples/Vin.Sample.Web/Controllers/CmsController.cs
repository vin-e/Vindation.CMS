using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vin.Core.Filters;
using Vin.Core.Services.Configuration;
using Vin.Sample.Business.Menus;
using Vin.Sample.Data.Model.Settings;
using Vin.Sample.Web.Models;

namespace Vin.Sample.Web.Controllers
{
    [TenantRequiredActionFilter]
    [RoutePrefix("Cms")]
    public class CmsController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly ISettingService _settingService;

        public CmsController(IMenuService menuService, ISettingService settingService)
        {
            this._menuService = menuService;
            this._settingService = settingService;
        }

        [ChildActionOnly, Route]
        public PartialViewResult RenderName()
        {
            var settings = new SettingsViewModel();
            settings.Company = _settingService.LoadSetting<CompanySettings>();

            return PartialView(settings);
        }

        [ChildActionOnly, Route]
        public PartialViewResult RenderMenu()
        {
            var menu = _menuService.Table.OrderBy(x => x.ID).FirstOrDefault();
            var menuItems = new List<MenuItemViewModel>();

            menu.Items
                .Where(x => x.ParentMenu == null)
                .ToList()
                .ForEach(x =>
                {
                    var item = new MenuItemViewModel();
                    item.Name = x.Information.MetaTitle;
                    item.PermaLink = string.IsNullOrEmpty(x.Information.Permalink) ? "/" : x.Information.Permalink.StartsWith("/") ? x.Information.Permalink : "/" + x.Information.Permalink;
                    
                    if (menu.Items.Any(a => a.ParentMenu != null && a.ParentMenu.ID == x.ID))
                    {
                        item.Items = new List<MenuItemViewModel>();
                        var subItems = menu.Items.Where(a => a.ParentMenu != null && a.ParentMenu.ID == x.ID);
                        foreach(var subItem in subItems)
                        {
                            item.Items.Add(new MenuItemViewModel()
                            {
                                Name = subItem.Information.MetaTitle,
                                PermaLink = string.IsNullOrEmpty(subItem.Information.Permalink) ? "/" : subItem.Information.Permalink.StartsWith("/") ? subItem.Information.Permalink : "/" + subItem.Information.Permalink
                            });
                        }
                    }

                    menuItems.Add(item);
                });

            return PartialView(menuItems);
        }

        [ChildActionOnly, Route]
        public PartialViewResult RenderFooter()
        {
            var settings = new SettingsViewModel();
            settings.Company = _settingService.LoadSetting<CompanySettings>();
            settings.Site = _settingService.LoadSetting<SiteSettings>();
            settings.Social = _settingService.LoadSetting<SocialSettings>();

            return PartialView(settings);
        }

        [ChildActionOnly, Route]
        public PartialViewResult RenderShareBar()
        {
            return PartialView();
        }
    }
}