using System.Linq;
using System.Collections.Generic;
using System;
namespace Server.UserDatabase
{
    public class DbInteraction
    {

        public static List<string> Users
        {
            get
            {
                using (UserDBContext db = new UserDBContext())
                {
                    return db.Users
                        .ToList()
                        .Select(a => a.NickName)
                        .ToList();
                }
            }
        }
        public static void CheckLinks()
        {
            using(UserDBContext db = new UserDBContext())
            {
                var unusedLinks = db
                    .LinkLibrary
                    .ToList()
                    .Where(a => a.NickName == "");
                if(unusedLinks.Count() == 0 || db.Users.Count() == 0)
                    return;
                var minLinks = db
                    .Users
                    .ToList()
                    .Select(a => Links(a.NickName).Count)
                    .Min();
                var minUsers = db.Users.ToList().Where(a => Links(a.NickName).Count == minLinks);
                foreach(var u in minUsers)
                {
                    foreach (var l in unusedLinks)
                    {
                        if (l.NickName != string.Empty)
                            continue;
                        l.NickName = u.NickName;
                        break;
                    }
                }
                unusedLinks = db
                    .LinkLibrary
                    .ToList()
                    .Where(a => a.NickName == "");
                while(unusedLinks.Count() != 0)
                {
                    foreach (var u in db.Users.ToList())
                    {
                        foreach (var l in unusedLinks)
                        {
                            if (l.NickName != string.Empty)
                                continue;
                            l.NickName = u.NickName;
                            break;
                        }
                    }
                }
            }
        }
        public static List<string> Links(string name)
        {
            using (UserDBContext db = new UserDBContext())
            {
                return db
                    .LinkLibrary
                    .ToList()
                    .Where(a => a.NickName == name)
                    .Select(a => a.Link)
                    .ToList();
            }
        }
        public static void Register
            (string nickname, string email, string password)
        {
            using(UserDBContext db = new UserDBContext())
            {
                db.Add(new UserInfo(email, nickname, password));
                db.SaveChanges();
            }
        }
        public static bool IsValidAuthorization(string nickname, string pass)
        {
            using(UserDBContext db = new UserDBContext())
            {
                var check = db.Users
                    .ToList()
                    .FirstOrDefault
                    (a => a.NickName == nickname);
                if (check == default(UserInfo))
                    return false;
                if (check.Password == pass)
                    check.IsLoggedIn = true;
                db.SaveChanges();
                return check.IsLoggedIn;
            }
        }
        public static void LogOut(string nickname)
        {
            using(UserDBContext db = new UserDBContext())
            {
                var check = db.Users
                    .ToList()
                    .FirstOrDefault(a => a.NickName == nickname);
                check.IsLoggedIn =
                    check == default(UserInfo) ?
                    throw new ArgumentException("No such nickname") :
                    false;
            }
        }
        protected static void AddLink(string link)
        {
            using(UserDBContext db = new UserDBContext())
            {
                db.Add(new Links(link));
                db.SaveChanges();
            }
        }
    }
}
