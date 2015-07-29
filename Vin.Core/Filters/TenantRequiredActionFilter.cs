using Autofac;
using Autofac.Integration.Owin;
using System.Web;
using System.Web.Mvc;
using Vin.Core.Model.MultiTenancy;
using Vin.Core.MultiTenancy;
using Vin.Core.Services.Configuration;

namespace Vin.Core.Filters
{
    public class TenantRequiredActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext.GetOwinContext();
            var tenantInstance = context.GetTenantInstance();

            if (tenantInstance == null)
                filterContext.Result = new RedirectResult("~/Error/UnknownTenant");

            var lifetimeScope = context.GetAutofacLifetimeScope();
            var settingService = lifetimeScope.Resolve<ISettingService>();
            var tenantSettings = settingService.LoadSetting<TenantSettings>();

            if (!tenantSettings.IsEnabled)
                filterContext.Result = new RedirectResult("~/Error/Maintenance");

            base.OnActionExecuting(filterContext);
        }
    }
}
