using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace Vin.Core.MultiTenancy
{
    public class TenantOwinAdapter : OwinMiddleware
    {
        private readonly ITenantEngine engine;

        public TenantOwinAdapter(OwinMiddleware next, ITenantEngine engine) : base(next)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            this.engine = engine;
        }

        public async override Task Invoke(IOwinContext context)
        {
            await engine.BeginRequest(context);
            await Next.Invoke(context);
            await engine.EndRequest(context);
        }
    }
}
