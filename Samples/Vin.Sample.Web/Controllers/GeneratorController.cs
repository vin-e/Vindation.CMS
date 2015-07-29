using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Vin.Core.MultiTenancy;

namespace Vin.Sample.Web.Controllers
{
    public class GeneratorController : ApiController
    {
        public dynamic Post(dynamic content)
        {
            var context = HttpContext.Current.GetOwinContext();
            var tenantInstance = context.GetTenantInstance();
            if (tenantInstance != null)
                return tenantInstance.Tenant.Name;

            return null;
        }
    }
}
