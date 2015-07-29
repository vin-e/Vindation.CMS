using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vin.Core.Filters;
using Vin.Core.Services.Configuration;
using Vin.Sample.Business.Posts;
using Vin.Sample.Data.Model.Posts;
using Vin.Sample.Web.Models;

namespace Vin.Sample.Web.Controllers
{
    [RoutePrefix("blog")]
    [TenantRequiredActionFilter]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ISettingService _settingService;

        public PostController(IPostService postService, ISettingService settingService)
        {
            this._postService = postService;
            this._settingService = settingService;
        }

        [Route]
        public ActionResult Index(int page = 1)
        {
            var postsVM = new PostsViewModel()
            {
                Categories = new List<string>(),
                PopularPosts = new List<PostViewModel>(),
                Posts = new List<PostViewModel>(),
                RecentPosts = new List<PostViewModel>(),
                Tags = new List<string>()
            };
            BuildAdditionalInformation(postsVM);

            var posts = _postService.Table.Where(p => p.PublishDate <= DateTime.UtcNow);

            postsVM.TotalPosts = posts.Count();
            postsVM.TotalPages = (int)Math.Ceiling(postsVM.TotalPosts * 1.0 / postsVM.PostsPerPage);
            postsVM.CurrentPage = page;

            foreach (var post in posts.OrderByDescending(ob => ob.PublishDate).Skip((page - 1) * postsVM.PostsPerPage).Take(postsVM.PostsPerPage))
                postsVM.Posts.Add(new PostViewModel(post));

            return View(postsVM);
        }

        [Route("{id?}")]
        public ActionResult SinglePost(string id)
        {
            ///Replace with 404 of the future
            if (string.IsNullOrEmpty(id))
                return RedirectToRoute("~/");

            Post post = _postService.Table.FirstOrDefault(p => (p.Meta.Permalink == id || p.Meta.Permalink == "blog/" + id) && p.PublishDate <= DateTime.UtcNow);

            if (post == null || post.ID <= 0)
                return Redirect("~/Error/404");

            var postsVM = new PostsViewModel()
            {
                Categories = new List<string>(),
                PopularPosts = new List<PostViewModel>(),
                Posts = new List<PostViewModel>() { new PostViewModel(post) },
                RecentPosts = new List<PostViewModel>(),
                Tags = new List<string>()
            };
            BuildAdditionalInformation(postsVM);

            HttpContext.Items["Vin_Meta"] = _postService.GetMetaData(post);
            return View(postsVM);
        }

        [Route("Search"), HttpGet]
        public ActionResult Search(string query, int page = 1)
        {
            var take = 10;
            var skip = ((page - 1) * take);

            var postsVM = new PostsViewModel()
            {
                Categories = new List<string>(),
                PopularPosts = new List<PostViewModel>(),
                Posts = new List<PostViewModel>(),
                RecentPosts = new List<PostViewModel>(),
                Tags = new List<string>()
            };
            BuildAdditionalInformation(postsVM);

            var posts = _postService.Table.Where(p => p.Title.Contains(query) || p.TextBody.Contains(query));

            postsVM.TotalPosts = posts.Count();
            postsVM.TotalPages = (int)Math.Ceiling(postsVM.TotalPosts * 1.0 / take);
            postsVM.CurrentPage = page;

            foreach (var post in posts.OrderByDescending(ob => ob.PublishDate).Skip(skip).Take(take))
                postsVM.Posts.Add(new PostViewModel(post));

            return View("~/Views/Post/Index.cshtml", postsVM);
        }

        [Route("category/{id}"), HttpGet]
        public ActionResult Category(string id, int page = 1)
        {
            var take = 10;
            var skip = ((page - 1) * take);

            var postsVM = new PostsViewModel()
            {
                Categories = new List<string>(),
                PopularPosts = new List<PostViewModel>(),
                Posts = new List<PostViewModel>(),
                RecentPosts = new List<PostViewModel>(),
                Tags = new List<string>()
            };
            BuildAdditionalInformation(postsVM);

            var posts = _postService.Table.Where(p => p.Categories.Any(x => x.Name == id));

            postsVM.TotalPosts = posts.Count();
            postsVM.TotalPages = (int)Math.Ceiling(postsVM.TotalPosts * 1.0 / take);
            postsVM.CurrentPage = page;

            foreach (var post in posts.OrderByDescending(ob => ob.PublishDate).Skip(skip).Take(take))
                postsVM.Posts.Add(new PostViewModel(post));

            return View("~/Views/Post/Index.cshtml", postsVM);
        }

        [Route("tag/{id}"), HttpGet]
        public ActionResult Tag(string id, int page = 1)
        {
            var take = 10;
            var skip = ((page - 1) * take);

            var postsVM = new PostsViewModel()
            {
                Categories = new List<string>(),
                PopularPosts = new List<PostViewModel>(),
                Posts = new List<PostViewModel>(),
                RecentPosts = new List<PostViewModel>(),
                Tags = new List<string>()
            };
            BuildAdditionalInformation(postsVM);

            var posts = _postService.Table.Where(p => p.Tags.Any(x => x.Name == id));

            postsVM.TotalPosts = posts.Count();
            postsVM.TotalPages = (int)Math.Ceiling(postsVM.TotalPosts * 1.0 / take);
            postsVM.CurrentPage = page;

            foreach (var post in posts.OrderByDescending(ob => ob.PublishDate).Skip(skip).Take(take))
                postsVM.Posts.Add(new PostViewModel(post));

            return View("~/Views/Post/Index.cshtml", postsVM);
        }

        private void BuildAdditionalInformation(PostsViewModel postsVM)
        {
            var postSettings = _settingService.LoadSetting<PostSettings>();
            postsVM.CommentsEnabled = postSettings.CommentsEnabled;
            postsVM.PostsPerPage = postSettings.PostsPerPage == 0 ? 5 : postSettings.PostsPerPage;
            postsVM.ShowPopularPosts = postSettings.ShowPopularPosts;
            postsVM.ShowRecentPosts = postSettings.ShowRecentPosts;

            var posts = _postService.Table.Where(p => p.PublishDate <= DateTime.UtcNow);

            if (postsVM.ShowRecentPosts)
                foreach (var post in posts.OrderByDescending(ob => ob.PublishDate).Take(3))
                    postsVM.RecentPosts.Add(new PostViewModel(post));

            if (postsVM.ShowPopularPosts)
                foreach (var post in posts.OrderBy(ob => Guid.NewGuid()).Take(3))
                    postsVM.PopularPosts.Add(new PostViewModel(post));

            var tags = posts.SelectMany(s => s.Tags.Distinct());
            if (tags.Count() > 0)
                tags.GroupBy(gb => gb.Name).ToList().ForEach(t => postsVM.Tags.Add(t.Key));

            var cats = posts.SelectMany(s => s.Categories.Distinct());
            if (cats.Count() > 0)
                cats.GroupBy(gb => gb.Name).ToList().ForEach(c => postsVM.Categories.Add(c.Key));
        }
    }
}