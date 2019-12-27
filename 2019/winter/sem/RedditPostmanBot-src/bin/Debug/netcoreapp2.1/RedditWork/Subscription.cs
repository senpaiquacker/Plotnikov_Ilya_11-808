using System;
using System.Collections.Generic;

namespace ContentSubscriptions
{
    class Subscription
    {
        public Func<string, IEnumerable<IMediaContent>> Content { get; }
        public Func<string, bool> IsKeyCorrect { get; }

        public Subscription(Func<string, IEnumerable<IMediaContent>> content)
        {
            Content = content;
        }

        public Subscription() { }
    }
}
