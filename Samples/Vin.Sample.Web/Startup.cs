using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.Owin;
using Autofac.Integration.WebApi;
using AutoMapper;
using AutoMapper.Mappers;
using Owin;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Vin.Core.Data;
using Vin.Core.Data.DataProviders;
using Vin.Core.Data.Repositories;
using Vin.Core.Model.MultiTenancy;
using Vin.Core.MultiTenancy;
using Vin.Core.Services.Configuration;
using Vin.Core.Services.Location;
using Vin.Core.Services.MultiTenancy;
using Vin.Core.Services.Themes;
using Vin.Sample.Business.Events;
using Vin.Sample.Business.Marketing;
using Vin.Sample.Business.Medias;
using Vin.Sample.Business.Menus;
using Vin.Sample.Business.Pages;
using Vin.Sample.Business.Posts;
using Vin.Sample.Data;

namespace Vin.Sample.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var engine = new TenantEngine(new TenantConfiguration
            {
                IdentificationStrategy = RequestIdentificationStrategies.FromHostname,
                TenantResolver = new SampleResolver()
            });

            var builder = new ContainerBuilder();
            builder.Register(x => new EfDataProviderManager()).As<BaseDataProviderManager>().InstancePerDependency();
            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            var efDataProviderManager = new EfDataProviderManager();
            var dataProvider = efDataProviderManager.LoadDataProvider();

            builder.Register<IDbContext>(c => new DomainObjectContext()).InstancePerHttpRequest();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();

            //Core
            builder.RegisterType<TenantService>().As<ITenantService>().InstancePerHttpRequest();
            builder.RegisterType<ThemeService>().As<IThemeService>().InstancePerHttpRequest();
            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerHttpRequest();
            builder.RegisterType<StateService>().As<IStateService>().InstancePerLifetimeScope();

            //Web and Api Controllers auto config
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //Sample Services
            builder.RegisterType<ContactService>().As<IContactService>().InstancePerHttpRequest();
            builder.RegisterType<EventService>().As<IEventService>().InstancePerHttpRequest();
            builder.RegisterType<MediaService>().As<IMediaService>().InstancePerHttpRequest();
            builder.RegisterType<MenuService>().As<IMenuService>().InstancePerHttpRequest();
            builder.RegisterType<NewsletterService>().As<INewsletterService>().InstancePerHttpRequest();
            builder.RegisterType<PageService>().As<IPageService>().InstancePerHttpRequest();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerHttpRequest();
            
            var container = builder.Build();
            var dependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseMultiTenancy(engine);
        }
    }

    public class SampleResolver : ITenantResolver
    {
        private ITenantService _tenantService;

        public SampleResolver()
        {
        }

        public Task<Tenant> Resolve(string tenantIdentifier)
        {
            var context = HttpContext.Current.GetOwinContext();
            var lifetimeScope = context.GetAutofacLifetimeScope();
            _tenantService = lifetimeScope.Resolve<ITenantService>();

            var tenant = _tenantService.GetByIdentifier(tenantIdentifier);
                        
            return Task.FromResult<Tenant>(tenant);
        }
    }
}