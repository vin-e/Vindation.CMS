Vindation 
=========
*Because no one is more bad ass than Vin Diesel*


The project builds upon a core foundation (Vin.Core) for quick development of a web based (MVC, Web Api) application
Current Vin.Core features:
* Entity Framework integration
* Repository Pattern
* Default dependency injection registrar via AutoFac
* Multi-Tenancy with configurable method to determine a tenant (*Vin.Core.MultiTenancy.RequestIdentificationStrategies*)
* Theme's based on Tenant via TenantSettings. Theme's are configured a configuration setting (VinConfig) in web.config, a setting in TenantSettings and details configured in theme.json within the theme folder
* Tenant/Feature based settings (see: *Vin.Core.Model.MultiTenancy.TenantSettings*)
	
The sample project(s) complete the application is a blog/cms
* Vin.Sample.Data provides the default DBContext, Models, Mapping and Migrations
* Vin.Sample.Business provides the interface that either Web Api and/or MVC application interact with the models
* Vin.Sample.Web provides the default web interface
* Note: Theme's for the sample will be limited as we will have implemented a better theme for our internal uses of this application.

Acknowledgements:
* The multi-tenancy is based on the project SaasKit (https://github.com/saaskit/saaskit)
* The repository, dependency injection and setting implementation is influenced by NopCommerce's implementation of the features.