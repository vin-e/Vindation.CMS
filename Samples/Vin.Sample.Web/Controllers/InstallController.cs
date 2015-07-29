using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vin.Core.Model.Locations;
using Vin.Core.Model.MultiTenancy;
using Vin.Core.MultiTenancy;
using Vin.Core.Services.Configuration;
using Vin.Core.Services.Location;
using Vin.Core.Services.MultiTenancy;
using Vin.Sample.Business.Events;
using Vin.Sample.Business.Menus;
using Vin.Sample.Business.Pages;
using Vin.Sample.Business.Posts;
using Vin.Sample.Data.Model;
using Vin.Sample.Data.Model.Events;
using Vin.Sample.Data.Model.Menus;
using Vin.Sample.Data.Model.Pages;
using Vin.Sample.Data.Model.Posts;
using Vin.Sample.Data.Model.Settings;

namespace Vin.Sample.Web.Controllers
{
    [RoutePrefix("install")]
    public class InstallController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IMenuService _menuService;
        private readonly IPageService _pageService; 
        private readonly IPostService _postService;
        private readonly ISettingService _settingService;
        private readonly IStateService _stateService;
        private readonly ITenantService _tenantService;
        
        public InstallController(IEventService eventService, IMenuService menuService, IPageService pageService, IPostService postService, ISettingService settingService, 
            IStateService stateService, ITenantService tenantService)
        {
            _eventService = eventService;
            _menuService = menuService;
            _pageService = pageService;
            _postService = postService;
            _settingService = settingService;
            _stateService = stateService;
            _tenantService = tenantService;
        }

        [Route]
        public ActionResult Index()
        {
            if (_stateService.Table.Count() == 0)
            {
                #region State List Add
                var stateList = new List<State>
                    {
                        new State() { Name = "Alabama", Abbreviation = "AL" },
                        new State() { Name = "Alaska", Abbreviation = "AK" },
                        new State() { Name = "Arizona", Abbreviation = "AZ" },
                        new State() { Name = "Arkansas", Abbreviation = "AR" },
                        new State() { Name = "California", Abbreviation = "CA" },
                        new State() { Name = "Colorado", Abbreviation = "CO" },
                        new State() { Name = "Connecticut", Abbreviation = "CT" },
                        new State() { Name = "Delaware", Abbreviation = "DE" },
                        new State() { Name = "District of Columbia", Abbreviation = "DC" },
                        new State() { Name = "Florida", Abbreviation = "FL" },
                        new State() { Name = "Georgia", Abbreviation = "GA" },
                        new State() { Name = "Hawaii", Abbreviation = "HI" },
                        new State() { Name = "Idaho", Abbreviation = "ID" },
                        new State() { Name = "Illinois", Abbreviation = "IL" },
                        new State() { Name = "Indiana", Abbreviation = "IN" },
                        new State() { Name = "Iowa", Abbreviation = "IA" },
                        new State() { Name = "Kansas", Abbreviation = "KS" },
                        new State() { Name = "Kentucky", Abbreviation = "KY" },
                        new State() { Name = "Louisiana", Abbreviation = "LA" },
                        new State() { Name = "Maine", Abbreviation = "ME" },
                        new State() { Name = "Maryland", Abbreviation = "MD" },
                        new State() { Name = "Massachusetts", Abbreviation = "MA" },
                        new State() { Name = "Michigan", Abbreviation = "MI" },
                        new State() { Name = "Minnesota", Abbreviation = "MN" },
                        new State() { Name = "Mississippi", Abbreviation = "MS" },
                        new State() { Name = "Missouri", Abbreviation = "MO" },
                        new State() { Name = "Montana", Abbreviation = "MT" },
                        new State() { Name = "Nebraska", Abbreviation = "NE" },
                        new State() { Name = "Nevada", Abbreviation = "NV" },
                        new State() { Name = "New Hampshire", Abbreviation = "NH" },
                        new State() { Name = "New Jersey", Abbreviation = "NJ" },
                        new State() { Name = "New Mexico", Abbreviation = "NM" },
                        new State() { Name = "New York", Abbreviation = "NY" },
                        new State() { Name = "North Carolina", Abbreviation = "NC" },
                        new State() { Name = "North Dakota", Abbreviation = "ND" },
                        new State() { Name = "Ohio", Abbreviation = "OH" },
                        new State() { Name = "Oklahoma", Abbreviation = "OK" },
                        new State() { Name = "Oregon", Abbreviation = "OR" },
                        new State() { Name = "Pennsylvania", Abbreviation = "PA" },
                        new State() { Name = "Puerto Rico", Abbreviation = "PR" },
                        new State() { Name = "Rhode Island", Abbreviation = "RI" },
                        new State() { Name = "South Carolina", Abbreviation = "SC" },
                        new State() { Name = "South Dakota", Abbreviation = "SD" },
                        new State() { Name = "Tennessee", Abbreviation = "TN" },
                        new State() { Name = "Texas", Abbreviation = "TX" },
                        new State() { Name = "Utah", Abbreviation = "UT" },
                        new State() { Name = "Vermont", Abbreviation = "VT" },
                        new State() { Name = "Virgin Islands", Abbreviation = "VI" },
                        new State() { Name = "Virginia", Abbreviation = "VA" },
                        new State() { Name = "Washington", Abbreviation = "WA" },
                        new State() { Name = "West Virginia", Abbreviation = "WV" },
                        new State() { Name = "Wisconsin", Abbreviation = "WI" },
                        new State() { Name = "Wyoming", Abbreviation = "WY" }
                    };

                stateList.ForEach(x => _stateService.Insert(x));
                #endregion
            }

            if (!_tenantService.Table.Any(x => x.Name == "Acme"))
            {
                _tenantService.Insert(new Tenant()
                {
                    Name = "Acme",
                    RequestIdentifiers = new List<TenantIdentifier>() { new TenantIdentifier() { Name = "acme.britereach.com", IsDefault = true } }
                });
            }

            if (!_tenantService.Table.Any(x => x.Name == "Do Good"))
            {
                _tenantService.Insert(new Tenant()
                {
                    Name = "Do Good",
                    RequestIdentifiers = new List<TenantIdentifier>() { new TenantIdentifier() { Name = "dogood.britereach.com", IsDefault = true } }
                });
            }

            if (!_tenantService.Table.Any(x => x.Name == "Local"))
            {
                _tenantService.Insert(new Tenant()
                {
                    Name = "Local",
                    RequestIdentifiers = new List<TenantIdentifier>() { new TenantIdentifier() { Name = "localhost", IsDefault = true } }
                });
            }

            return View();
        }

        [HttpPost, Route]
        public ActionResult Index(string password, string theme)
        {
            if (string.IsNullOrEmpty(password) || password != "test")
                return View();

            var tenantInstance = HttpContext.GetOwinContext().GetTenantInstance();

            #region Settings
            theme = string.IsNullOrEmpty(theme) ? "Default" : theme;
            _settingService.SaveSetting(new TenantSettings()
            {
                DefaultTheme = theme,
                IsEnabled = true
            });

            _settingService.SaveSetting(new SiteSettings()
            {
                EnableNewsletter = true,
                EnableTwitterFeed = true,
                EnableGoogleAnalytics = true,
                GoogleAnaltyicsId = "UA-31163101-2"
            });
            
            _settingService.SaveSetting(new CompanySettings()
            {
                Address = "125 Paseo de la Plaza",
                Brief = string.Format("At {0} we are dedicated to doing good.", tenantInstance.Tenant.Name),
                City = "Los Angeles",
                Ein = "11-1111111", 
                Email = "email@company.com",
                Name = tenantInstance.Tenant.Name,
                State = "CA",
                ZipCode = "90012",
                Phone = "213-555-5555"
            });
            
            _settingService.SaveSetting(new SocialSettings()
            {
                Facebook = "https://www.facebook.com/visualstudio.us",
                GooglePlus = "https://plus.google.com/+visualstudio",
                LinkedIn = "https://www.linkedin.com/company/microsoft-visual-studio",
                Pinterest = "http://www.pinterest.com/msvisualstudio/",
                Twitter = "https://twitter.com/VisualStudio",
                YouTube = "https://www.youtube.com/user/VisualStudio"
            });

            _settingService.SaveSetting(new PostSettings()
            {
                CommentsEnabled = true,
                PostsPerPage = 5,
                ShowPopularPosts = true,
                ShowRecentPosts = true
            });
            #endregion
            
            #region Home Page
            var HomePage = new Page()
                {
                    HtmlBody = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Tenant_ID = tenantInstance.Tenant.ID,
                    TextBody = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Title = tenantInstance.Tenant.Name + " Home Page", 
                };

            var metaPage1 = new MetaData()
            {
                EntityType = "Page",
                EntityID = HomePage.ID,
                IsHomePage = true,
                MetaDescription = "Home local host test page",
                MetaKeywords = "CMS, localhost, testing",
                MetaTitle = "Home",
                Permalink = "",
                Tenant_ID = tenantInstance.Tenant.ID
            };

            HomePage.Meta = metaPage1;
            _pageService.Insert(HomePage);

            HomePage.Meta.EntityID = HomePage.ID;
            _pageService.Update(HomePage);
            #endregion

            #region About Page
            var Page2 = new Page()
            {
                HtmlBody = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Tenant_ID = tenantInstance.Tenant.ID,
                TextBody = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Title =  tenantInstance.Tenant.Name + " About",
            };

            var metaPage2 = new MetaData()
            {
                EntityType = "Page",
                EntityID = Page2.ID,
                IsHomePage = false,
                MetaDescription = "local host test page 2",
                MetaKeywords = "CMS, localhost, testing",
                MetaTitle = "About",
                Permalink = "about",
                Tenant_ID = tenantInstance.Tenant.ID
            };

            Page2.Meta = metaPage2;
            _pageService.Insert(Page2);

            Page2.Meta.EntityID = Page2.ID;
            _pageService.Update(Page2);
            #endregion

            #region About_Leadership Page
            var Page3 = new Page()
            {
                HtmlBody = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Tenant_ID = tenantInstance.Tenant.ID,
                TextBody = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Title = tenantInstance.Tenant.Name + " Leadership",
                ParentPage = Page2
            };

            var metaPage3 = new MetaData()
            {
                EntityType = "Page",
                EntityID = Page3.ID,
                IsHomePage = false,
                MetaDescription = "Leadership test page",
                MetaKeywords = "CMS, localhost, testing",
                MetaTitle = "Leadership",
                Permalink = "about/leadership",
                Tenant_ID = tenantInstance.Tenant.ID
            };

            Page3.Meta = metaPage3;
            _pageService.Insert(Page3);

            Page3.Meta.EntityID = Page3.ID;
            _pageService.Update(Page3);
            #endregion

            #region Additional Page
            var Page4 = new Page()
            {
                HtmlBody = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Tenant_ID = tenantInstance.Tenant.ID,
                TextBody = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Title = tenantInstance.Tenant.Name + " Additional Page",
                ParentPage = Page2
            };

            var metaPage4 = new MetaData()
            {
                EntityType = "Page",
                EntityID = Page4.ID,
                IsHomePage = false,
                MetaDescription = "Leadership test page",
                MetaKeywords = "CMS, localhost, testing",
                MetaTitle = "Additional Page",
                Permalink = "about/additional_page",
                Tenant_ID = tenantInstance.Tenant.ID
            };

            Page4.Meta = metaPage4;
            _pageService.Insert(Page4);

            Page4.Meta.EntityID = Page4.ID;
            _pageService.Update(Page4);
            #endregion
            
            #region Posts
            ///Adding 101 posts
            
            for (int i = 1; i <= 101; i++)
            {
                var post = new Post()
                    {
                        HtmlBody = "<strong>Lorem ipsum</strong> dolor sit amet, <p>consectetur adipisicing elit, sed do eiusmod tempor incididunt ut <i>labore et dolore</i> magna aliqua.</p> Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        PublishDate = DateTime.UtcNow,
                        TextBody = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        Title = string.Format("Dynamic Post {0}", i),
                        Meta = new MetaData()
                        {
                            EntityType = "Post",
                            EntityID = 0,
                            IsHomePage = false,
                            MetaDescription = string.Format("test post {0}", i),
                            MetaKeywords = "CMS, testing",
                            MetaTitle = string.Format("Post {0}", i),
                            Permalink = string.Format("blog/post-{0}", i),
                            Tenant_ID = tenantInstance.Tenant.ID
                        }, 
                        Categories = new List<Category>(), 
                        Tags = new List<Tag>()
                    };
                if (i % 4 == 0)
                {
                    var cat4 = _postService.Table.Any(x => x.Categories.Any(y => y.Name == "Sports")) ?
                        _postService.Table.First(x => x.Categories.Any(y => y.Name == "Sports")).Categories.First(c => c.Name == "Sports") :
                        new Category() { Name = "Sports", Tenant_ID = tenantInstance.Tenant.ID };
                    var tag4 = _postService.Table.Any(x => x.Tags.Any(y => y.Name == "fantasy football")) ?
                        _postService.Table.First(x => x.Tags.Any(y => y.Name == "fantasy football")).Tags.First(c => c.Name == "fantasy football") : 
                        new Tag() { Name = "fantasy football", Tenant_ID = tenantInstance.Tenant.ID };

                    post.Categories.Add(cat4);
                    post.Tags.Add(tag4);
                }
                else if (i % 3 == 0)
                {
                    var cat3 = _postService.Table.Any(x => x.Categories.Any(y => y.Name == "Politics")) ?
                        _postService.Table.First(x => x.Categories.Any(y => y.Name == "Politics")).Categories.First(c => c.Name == "Politics") : 
                        new Category() { Name = "Politics", Tenant_ID = tenantInstance.Tenant.ID };
                    var tag3 = _postService.Table.Any(x => x.Tags.Any(y => y.Name == "tea party")) ?
                        _postService.Table.First(x => x.Tags.Any(y => y.Name == "tea party")).Tags.First(c => c.Name == "tea party") : 
                        new Tag() { Name = "tea party", Tenant_ID = tenantInstance.Tenant.ID };
                    post.Categories.Add(cat3);
                    post.Tags.Add(tag3);
                }
                else if (i % 2 == 0)
                {
                    var cat2 = _postService.Table.Any(x => x.Categories.Any(y => y.Name == "Animals")) ?
                        _postService.Table.First(x => x.Categories.Any(y => y.Name == "Animals")).Categories.First(c => c.Name == "Animals") : 
                        new Category() { Name = "Animals", Tenant_ID = tenantInstance.Tenant.ID };
                    var tag2 = _postService.Table.Any(x => x.Tags.Any(y => y.Name == "koala")) ?
                        _postService.Table.First(x => x.Tags.Any(y => y.Name == "koala")).Tags.First(c => c.Name == "koala") : 
                        new Tag() { Name = "koala", Tenant_ID = tenantInstance.Tenant.ID };
                    post.Categories.Add(cat2);
                    post.Tags.Add(tag2);
                }
                else
                {
                    var cat1 = _postService.Table.Any(x => x.Categories.Any(y => y.Name == "Technology")) ?
                        _postService.Table.First(x => x.Categories.Any(y => y.Name == "Technology")).Categories.First(c => c.Name == "Technology") : 
                        new Category() { Name = "Technology", Tenant_ID = tenantInstance.Tenant.ID };
                    var tag1 = _postService.Table.Any(x => x.Tags.Any(y => y.Name == "c#")) ?
                        _postService.Table.First(x => x.Tags.Any(y => y.Name == "c#")).Tags.First(c => c.Name == "c#") : 
                        new Tag() { Name = "c#", Tenant_ID = tenantInstance.Tenant.ID };
                    post.Categories.Add(cat1);
                    post.Tags.Add(tag1);
                }

                _postService.Insert(post);
                post.Meta.EntityID = post.ID;
                _postService.Update(post);
            }
            #endregion

            #region Events
            //lots of events
            for (int i = 1; i <= 45; i++)
            {
                var evt = new Event()
                    {
                        Address = string.Format("{0} Street Ave.", i),
                        City = string.Format("City {0}", i),
                        Details = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        EndDate = DateTime.Now.AddDays(i * 2),
                        IsPublished = true,
                        Name = string.Format("Event {0}", i),
                        StartDate = DateTime.Now.AddDays(i * 2),
                        State = "CA",
                        Tenant_ID = tenantInstance.Tenant.ID,
                        ZipCode = "00000"
                    };

                _eventService.Insert(evt);
            }
            #endregion

            #region Menu
            var menu = new Menu();
            menu.Name = "Main menu";
            menu.Items = new List<MenuItem>();
            menu.Items.Add(new MenuItem() { Information = HomePage.Meta });
            var aboutMenu = new MenuItem() { Information = Page2.Meta };
            menu.Items.Add(aboutMenu);
            menu.Items.Add(new MenuItem() { Information = Page3.Meta, ParentMenu = aboutMenu });
            menu.Items.Add(new MenuItem() { Information = Page4.Meta });
            menu.Items.Add(new MenuItem() { Information = new MetaData() { EntityType = "Blog", MetaTitle = "Blog", Permalink = "blog", Tenant_ID = tenantInstance.Tenant.ID } });
            menu.Items.Add(new MenuItem() { Information = new MetaData() { EntityType = "Events", MetaTitle = "Events", Permalink = "events", Tenant_ID = tenantInstance.Tenant.ID } });
            menu.Items.Add(new MenuItem() { Information = new MetaData() { EntityType = "Contact", MetaTitle = "Contact", Permalink = "contact", Tenant_ID = tenantInstance.Tenant.ID } });
            _menuService.Insert(menu);
            //_menuService.Update(menu);
            #endregion

            return RedirectToAction("Completed");
        }

        [Route("Completed")]
        public ActionResult Completed()
        {
            return View();
        }
    }
}