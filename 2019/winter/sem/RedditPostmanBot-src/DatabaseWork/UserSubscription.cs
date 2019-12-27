using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseWorker
{
    public class UserSubscription
    {
        public string UserId { get; internal set; }
        public string Subscription { get; internal set; }
        public string SubscriptionKey { get; internal set; }
        public int Priority { get; internal set; }

        public UserSubscription() { }
        public UserSubscription(string uid, string sub, string subkey, int priority)
        {
            UserId = uid;
            Subscription = sub;
            SubscriptionKey = subkey;
            Priority = priority;
        }
        public override string ToString()
        {
            return "{ " + UserId + " - " + Subscription + " - " + SubscriptionKey + " }";
        }
    }
}
