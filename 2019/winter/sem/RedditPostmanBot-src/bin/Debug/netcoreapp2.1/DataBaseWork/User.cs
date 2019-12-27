using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DataBaseWorker
{
    public class User
    {
        public string UserId { get; internal set; }

        public int SubCount
        {
            get
            {
                using (BotContext db = new BotContext())
                {
                    return db
                    .UsersSubscriptions
                    .Where(sub => sub.UserId == this.UserId)
                    .ToList()
                    .Count;
                }
            }
        }

        public override string ToString()
        {
            return "{ " + UserId + " - " + SubCount.ToString() + " }";
        }
        public User() { }
        public User(string userId)
        {
            UserId = userId;
        }
    }
}
