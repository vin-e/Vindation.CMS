using Autofac;
using Autofac.Integration.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Vin.Core.MultiTenancy;
using Vin.Core.Services.Configuration;
using Vin.Core.Web;
using Vin.Sample.Data.Model.Settings;

namespace Vin.Sample.Web.Utilities
{
    public static class UI
    {
        private static UIHelper Helper = new UIHelper();

        /// <summary>
        /// Generates the tags appropriate for the html head.
        /// </summary>
        /// <returns>The head information</returns>
        public static IHtmlString Head() { return Helper.Head(); }

        /// <summary>
        /// Renders the files and tags associated with the footer
        /// </summary>
        /// <returns>Html string</returns>
        public static IHtmlString Footer()
        {
            var context = HttpContext.Current.GetOwinContext();
            var tenantInstance = context.GetTenantInstance();
            var lifetimeScope = context.GetAutofacLifetimeScope();
            var settingService = lifetimeScope.Resolve<ISettingService>();
            var siteSettings = settingService.LoadSetting<SiteSettings>();

            StringBuilder ga = new StringBuilder();
            if (siteSettings.EnableGoogleAnalytics)
            {
                ga.AppendLine("<script>");
                ga.AppendLine("(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){");
                ga.AppendLine("(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),");
                ga.AppendLine("m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)");
                ga.AppendLine("})(window,document,'script','//www.google-analytics.com/analytics.js','ga');");

                ga.AppendLine(string.Format("ga('create', '{0}', '{1}');", siteSettings.GoogleAnaltyicsId, tenantInstance.Tenant.RequestIdentifiers.Where(x => x.IsDefault).FirstOrDefault().Name));
                ga.AppendLine("ga('send', 'pageview');");
                ga.AppendLine("</script>");
            }

            return Helper.Footer(ga.ToString());
        }

        /// <summary>
        /// Generates a full site url from the virtual path.
        /// </summary>
        /// <param name="virtualpath">The virtual path.</param>
        /// <returns>The full site url</returns>
        public static IHtmlString SiteUrl(string virtualpath) { return Helper.SiteUrl(virtualpath); }

        /// <summary>
        /// Generates an absolute url from the virtual path or site url.
        /// </summary>
        /// <param name="url">The url</param>
        /// <returns>The absolute url</returns>
        public static IHtmlString AbsoluteUrl(string url) { return Helper.AbsoluteUrl(url); }

        /// <summary>
        /// Gets a list of selectlistitem's of states
        /// </summary>
        /// <returns>List<SelectListItem> of States</SelectListItem></returns>
        public static IEnumerable<SelectListItem> GetStatesList() { return Helper.GetStatesList(); }
    }
}