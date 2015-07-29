using System.Web;
using System.Web.Optimization;

namespace Vin.Sample.Web.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.validate.js",
                "~/scripts/jquery.validate.unobtrusive.js"
                ));

            bundles.Add(new StyleBundle("~/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/css/font-awesome.css",
                "~/Content/body.css"
                ));
        }
    }
}