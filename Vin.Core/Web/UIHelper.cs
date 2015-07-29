using Autofac;
using Autofac.Integration.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Vin.Core.MultiTenancy;
using Vin.Core.Services.Location;

namespace Vin.Core.Web
{
    public sealed class UIHelper
    {
        /// <summary>
        /// Returns values that are to be placed in header of document (i.e. title, meta keywords, meta description and theme css files
        /// </summary>
        /// <returns>Html string</returns>
        public IHtmlString Head()
        {
            var context = HttpContext.Current.GetOwinContext();
            var tenantInstance = context.GetTenantInstance();
            var metaData = (MetaInfo)HttpContext.Current.Items["Vin_Meta"];
            StringBuilder str = new StringBuilder();

            var title = (metaData != null && !string.IsNullOrEmpty(metaData.MetaTitle)) ? metaData.MetaTitle : "Default text here";
            str.AppendLine("<title>" + title + "</title>");

            str.AppendLine("<meta name=\"generator\" content=\"Brite Reach CMS\" />");
            str.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />");
            
            if (metaData != null)
            {
                if (!string.IsNullOrEmpty(metaData.MetaDescription))
                    str.AppendLine("<meta name=\"description\" content=\"" + metaData.MetaDescription + "\" />");
                if (!string.IsNullOrEmpty(metaData.MetaKeywords))
                    str.AppendLine("<meta name=\"keywords\" content=\"" + metaData.MetaKeywords + "\" />");
            }
            
            //TODO: Canonical tags
            //str.AppendLine(string.Format("<link rel='canonical' href='{0}/{1}' />", , ));
            
            //TODO: Open Graph and/or schema.org tags

            //Template
            if (tenantInstance != null)
            {
                var themePath = tenantInstance.Tenant.ThemeConfig.RelativePath;
                
                var cssFiles = new List<string>();
                foreach (var css in tenantInstance.Tenant.ThemeConfig.Css)
                {
                    if (css.StartsWith("http") || css.StartsWith("//"))
                        str.AppendLine("<link href=\"" + css + "\" rel=\"stylesheet\" />");
                    else
                        str.AppendLine("<link href=\"" + SiteUrl(themePath + "/" + css) + "\" rel=\"stylesheet\" />");
                }
            }

            return new HtmlString(str.ToString());
        }

        /// <summary>
        /// Renders the files and tags associated with the footer
        /// </summary>
        /// <returns>Html string</returns>
        public IHtmlString Footer(string additionalString = "")
        {
            StringBuilder str = new StringBuilder();

            //Template
            var context = HttpContext.Current.GetOwinContext();
            var tenantInstance = context.GetTenantInstance();

            if (tenantInstance != null)
            {
                var themePath = tenantInstance.Tenant.ThemeConfig.RelativePath;

                foreach (var js in tenantInstance.Tenant.ThemeConfig.Javascript)
                {
                    if (js.StartsWith("http") || js.StartsWith("//"))
                        str.AppendLine("<script src=\"" + js + "\" ></script>");
                    else
                        str.AppendLine("<script src=\"" + SiteUrl(themePath + "/" + js) + "\" ></script>");
                }
            }

            str.AppendLine(additionalString);

            return new HtmlString(str.ToString());
        }

        /// <summary>
        /// Generates a full site url from the virtual path.
        /// </summary>
        /// <param name="virtualpath">The virtual path.</param>
        /// <returns>The full site url</returns>
        public IHtmlString SiteUrl(string virtualpath)
        {
            return new HtmlString(Url(virtualpath));
        }

        /// <summary>
        /// Generates an absolute url from the virtual path or site url.
        /// </summary>
        /// <param name="url">The url</param>
        /// <returns>The absolute url</returns>
        public IHtmlString AbsoluteUrl(string url)
        {
            var request = HttpContext.Current.Request;

            // First, convert virtual paths to site url's
            if (url.StartsWith("~/"))
                url = SiteUrl(url).ToString(); ;

            // Now add server, scheme and port
            return new HtmlString(request.Url.Scheme + "://" + request.Url.DnsSafeHost +
                (!request.Url.IsDefaultPort ? ":" + request.Url.Port.ToString() : "") + url);
        }

        public IEnumerable<SelectListItem> GetStatesList()
        {
            var items = new List<SelectListItem>();

            var context = HttpContext.Current.GetOwinContext();
            var lifetimeScope = context.GetAutofacLifetimeScope();
            var stateService = lifetimeScope.Resolve<IStateService>();

            stateService.Table.ToList().ForEach(s => { items.Add(new SelectListItem() { Text = s.Name, Value = s.Abbreviation }); });

            return items;
        }

        #region Private Methods
        private string Url(string virtualpath)
        {
            var request = HttpContext.Current.Request;
            return virtualpath.Replace("~/", request.ApplicationPath + (request.ApplicationPath != "/" ? "/" : ""));
        }
        #endregion
    }
}
