using System.ComponentModel.DataAnnotations;
namespace DataBaseWorker
{
    public class UserReadPosts : TableRelation
    {
        [Key]
        public int RelationID { get; set; }
        internal string UserId { get; }
        internal readonly string PostToken;
        internal UserReadPosts()
        {

        }
        internal UserReadPosts(string userId, string postToken)
        {
            UserId = userId;
            PostToken = postToken;
        }
        public string ReturnRelation()
        {
            return "{ " + UserId + " - " + PostToken + " }";
        }
    }
}
