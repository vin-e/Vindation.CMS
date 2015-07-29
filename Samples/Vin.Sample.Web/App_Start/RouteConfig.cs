using System.Web.Mvc;
using System.Web.Routing;

namespace Vin.Sample.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Pages",
                url: "{*id}",
                defaults: new { controller = "Page", action = "Index", id = UrlParameter.Optional }
            );



            //routes.MapRoute(
            //    name: "IndexDefault",
            //    url: "{controller}/{id}",
            //    defaults: new { controller = "Page", action = "Index", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Default2",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Page", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
