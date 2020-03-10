using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Server.UserDatabase
{
    class UserInfo
    {
        [EmailAddress, Required]
        public string EMail { get; set; }
        [Required, MinLength(4), MaxLength(20)]
        public string NickName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsLoggedIn { get; set; }
        public UserInfo()
        {

        }
        public UserInfo(string mail, string nick, string pass)
        {
            EMail = mail;
            NickName = nick;
            Password = pass;
            using (UserDBContext db = new UserDBContext())
            {
                var links = db.LinkLibrary.ToList();
                var users = db.Users.ToList();
                if(users.Count == 0)
                {
                    foreach(var link in links)
                    {
                        link.NickName = NickName;
                    }
                }
                else
                {
                    int linksPerUser = links.Count / users.Count;
                    int userCount = 0;
                    while (linksPerUser > 0)
                    {
                        links
                            .Where(a => a.NickName == users[userCount].NickName)
                            .FirstOrDefault()
                            .NickName = NickName;
                        userCount++;
                        linksPerUser--;
                        if (userCount == users.Count)
                            userCount = 0;

                    }
                }
                db.SaveChanges();
            }
        }
    }
}
