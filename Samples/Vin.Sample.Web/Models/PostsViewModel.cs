using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vin.Sample.Data.Model.Posts;

namespace Vin.Sample.Web.Models
{
    public class PostsViewModel
    {
        public ICollection<PostViewModel> Posts { get; set; }

        public int TotalPosts { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public ICollection<string> Categories { get; set; }

        public ICollection<string> Tags { get; set; }

        public ICollection<PostViewModel> RecentPosts { get; set; }

        public ICollection<PostViewModel> PopularPosts { get; set; }

        //Settings
        public int PostsPerPage { get; set; }

        public bool CommentsEnabled { get; set; }

        public bool ShowRecentPosts { get; set; }

        public bool ShowPopularPosts { get; set; }
    }

    public class PostViewModel
    {
        public PostViewModel() { }
        public PostViewModel(Post post)
        {
            ID = post.ID;
            Title = post.Title;
            Url = post.Meta.Permalink;
            HtmlBody = post.HtmlBody;
            TextBody = post.TextBody;
            HeaderImage = post.HeaderImage != null ? post.HeaderImage.Url : string.Empty;
            PublishDate = post.PublishDate;
            CreatedDate = post.CreatedDate;
            UpdatedDate = post.UpdatedDate;
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string HtmlBody { get; set; }

        public string TextBody { get; set; }

        public bool AllowComments { get; set; }

        public int CommentCount { get; set; }

        public string HeaderImage { get; set; }

        public DateTime? PublishDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}