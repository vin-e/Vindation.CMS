using Vin.Core.Configuration;

namespace Vin.Sample.Data.Model.Posts
{
    public class PostSettings : ISettings
    {
        public int PostsPerPage { get; set; }

        public bool CommentsEnabled { get; set; }

        public bool ShowRecentPosts { get; set; }

        public bool ShowPopularPosts { get; set; }
    }
}
