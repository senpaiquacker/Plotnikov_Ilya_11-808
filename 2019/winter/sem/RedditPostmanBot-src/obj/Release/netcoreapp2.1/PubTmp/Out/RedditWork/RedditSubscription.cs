using System;
using System.Collections.Generic;
using System.Linq;
using Reddit;

namespace ContentSubscriptions
{
    class RedditSubscription : Subscription
    {
        public readonly static RedditClient RedditClient = RedditPost.RedditClient;

        public readonly static RedditSubscription RedditHot = new RedditSubscription(subreddit => RedditClient.Subreddit(subreddit).Posts.Hot
                    .Where(post => !RedditClient.Models.LinksAndComments.Info(post.Fullname).Posts.First().Stickied)
                    .Select(post => new RedditPost(post)));

        public new Func<string, IEnumerable<RedditPost>> Content { get; }
        public RedditSubscription(Func<string, IEnumerable<RedditPost>> content)
        {
            Content = content;
        }
    }
}
