using System.ComponentModel.DataAnnotations;
namespace DataBaseWorker
{
    public class User : TableRelation
    {
        [Key]
        public int RelationID { get; internal set; }
        public string UserId { get; internal set; }
        public string Subscription { get; internal set; }
        public string SubscriptionKey { get; internal set; }
        internal User()
        {

        }
        public User(string uid, string sub, string subkey)
        {
            UserId = uid;
            Subscription = sub;
            SubscriptionKey = subkey;
        }
        public string ReturnRelation()
        {
            return "{ " + UserId + " - " + Subscription + " - " + SubscriptionKey + " }";
        }
    }
}
