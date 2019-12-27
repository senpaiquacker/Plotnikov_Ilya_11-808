using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace DataBaseWorker
{
    public class UserSubCount : TableRelation
    {
        [Key]
        public int RelationID { get; internal set; }

        public string UserID { get; internal set; }
        public int Count
        {
            get
            {
                return DBOperational.GetUserSubList(UserID).ToList().Count;
            }
        }
        public string ReturnRelation()
        {
            return "{ " + UserID + " - " + Count.ToString() + " }";
        }
        public UserSubCount(string userId)
        {
            UserID = userId;
        }
        internal UserSubCount()
        {

        }
    }
}
