using System.Web;
using System.Web.Mvc;
using Vin.Core.Filters;

namespace Vin.Sample.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}