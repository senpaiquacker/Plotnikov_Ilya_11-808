using System.Linq;
using Reddit;

namespace ContentSubscriptions
{
    class RedditPost : IMediaContent
    {
        public readonly static RedditClient RedditClient = new RedditClient(accessToken: "410533437282-V_6oVeP1ca3jBxNPRh9RL0G8_z4", refreshToken: "410533437282-2EaFUQUH_RYtuN91jprzWujrGBc",
                appId: "B-2MVH28D7s9Dw", appSecret: "IRRX5bZhH9aeUP8f7zw06D4PQ3c");

        public Reddit.Controllers.Post Post { get; }
        public string UniqueID => Post.Fullname;
        public string ToMessage => RedditClient.Models.LinksAndComments.Info(Post.Fullname).Posts.First().URL;

        public RedditPost(Reddit.Controllers.Post post)
        {
            Post = post;
        }
    }
}
