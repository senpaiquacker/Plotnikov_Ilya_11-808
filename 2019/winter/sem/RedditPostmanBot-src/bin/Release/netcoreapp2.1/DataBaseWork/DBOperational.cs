using System;
using System.Collections.Generic;
using System.Linq;
namespace DataBaseWorker
{
    public class DBOperational
    {
        //Получает количество подписок пользователя
        public static int GetUserSubs(string userId)
        {
            using (BotContext db = new BotContext())
            {
                UserSubCount user = db
                    .UsersSubs
                    .ToList()
                    .FirstOrDefault(user => user.UserID == userId);
                db.SaveChanges();
                return user.Count;
                
            }
        }

        //получает список подписок пользователя
        public static List<string> GetUserSubList(string userId)
        {
            using (BotContext db = new BotContext())
            {
                db.SaveChanges();
                return db
                    .Users
                    .ToList()
                    .Where(user => user.UserId == userId)
                    .Select(user => user.Subscription + " " + user.SubscriptionKey)
                    .ToList();
            }
        }

        //добавляет нового пользователя
        public static void AddUser(string userId)
        {
            using (BotContext db = new BotContext())
            {
                var newUser = new UserSubCount(userId);
                if (db.UsersSubs.ToList().Exists(user => user == newUser))
                    return;
                db.UsersSubs.Add(newUser);
                db.SaveChanges();

            }
        }

        //добавляет новую подписку
        public static void AddSub(string userId, string sub, string subKey)
        {
            using (BotContext db = new BotContext())
            {
                if (db.Users
                    .ToList()
                    .Exists
                    (
                        user => user.UserId == userId && 
                        user.Subscription == sub && 
                        user.SubscriptionKey == subKey
                    ))
                    return;
                db.Users.Add(new User(userId, sub, subKey));
                db.SaveChanges();

            }
        }
        
        //убирает подписку
        public static void RemoveSub(string userId, string sub, string subKey)
        {
            using (BotContext db = new BotContext())
            {
                db.Users.Remove(new User(userId, sub, subKey));
                db.SaveChanges();
            }
        }
        
        //Убирает пользователя
        public static void RemoveUser(string userId)
        {
            using (BotContext db = new BotContext())
            {
                db.UsersSubs.Remove(new UserSubCount(userId));
                var removedSubs = db.Users.Where(user => user.UserId == userId);
                db.Users.RemoveRange(removedSubs.ToArray());
                db.SaveChanges();
            }
        }

        //добавляет пост в список прочитанных для данного пользователя
        public static bool AddReadPost(string userId, string postToken)
        {
            using(BotContext db = new BotContext())
            {
                var post = new UserReadPosts(userId, postToken);
                if (db.UsersPosts.ToList().Exists(a => a.UserId == userId && a.PostToken == postToken))
                    return false;
                db.UsersPosts.Add(post);
                db.SaveChanges();
            }
            return true;
        }
        

        public static void CleanReadPosts()
        {
            using (BotContext db = new BotContext())
            {
                db.UsersPosts.RemoveRange(db.UsersPosts);
                db.SaveChanges();
            }
        }
    }
}
