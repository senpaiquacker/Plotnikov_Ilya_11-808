using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseWorker
{
    public class UserSeenContent
    {
        [Key, Column(Order = 0)]
        public string UserId { get; internal  set; }

        [Key, Column(Order = 1)]
        public string ContentId { get; internal set; }
        public UserSeenContent() { }
        internal UserSeenContent(string userId, string postToken)
        {
            UserId = userId;
            ContentId = postToken;
        }

        public override string ToString()
        {
            return "{ " + UserId + " - " + ContentId + " }";
        }
    }
}
