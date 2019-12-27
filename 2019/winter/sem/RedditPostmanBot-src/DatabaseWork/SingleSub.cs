using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseWorker
{
    public class SingleSub
    {
        public string Subscription { get; }
        public string SubscriptionKey { get; }

        public SingleSub(string subscription, string subscriptionKey)
        {
            Subscription = subscription;
            SubscriptionKey = subscriptionKey;
        }
        public SingleSub(UserSubscription sub)
        {
            Subscription = sub.Subscription;
            SubscriptionKey = sub.SubscriptionKey;
        }
        public override bool Equals(object obj)
        {
            var subObj = obj as SingleSub;
            if (subObj == null) return false;
            return Subscription == subObj.Subscription
                && SubscriptionKey == subObj.SubscriptionKey;
        }

        public override int GetHashCode()
        {
            return (Subscription, SubscriptionKey).GetHashCode();
        }

        public override string ToString()
        {
            return "(" + Subscription + ": " + SubscriptionKey + ")";
        }
    }
}
