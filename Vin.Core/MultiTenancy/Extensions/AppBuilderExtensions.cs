using System;
using Vin.Core.MultiTenancy;

namespace Owin
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseMultiTenancy(this IAppBuilder app, ITenantEngine engine)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            return app.Use(typeof(TenantOwinAdapter), engine);
        }
    }
}
