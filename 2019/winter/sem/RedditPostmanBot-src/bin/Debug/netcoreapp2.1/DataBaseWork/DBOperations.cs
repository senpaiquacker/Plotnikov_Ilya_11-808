using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBaseWorker
{
    public static class DBOperations
    {
        //Получает количество подписок пользователя
        public static int GetUserSubCount(string userId)
        {
            using (BotContext db = new BotContext())
            {
                db.SaveChanges();
                User user = db.Users.FirstOrDefault(user => user.UserId == userId);
                if (user == null)
                    throw new ArgumentException($"User {userId} doesn't exist");
                return user.SubCount;
            }
        }

        //получает список подписок пользователя
        internal static IEnumerable<SingleSub> GetUserSubs(string userId)
        {
            using (BotContext db = new BotContext())
            {
                db.SaveChanges();
                User user = db.Users.FirstOrDefault(user => user.UserId == userId);
                if (user == null)
                    throw new ArgumentException($"User {userId} doesn't exist");
                return db
                    .UsersSubscriptions
                    .Where(sub => sub.UserId == userId)
                    .Select(sub => new SingleSub(sub.Subscription, sub.SubscriptionKey));
            }
        }

        //получает нужную подписку
        public static SingleSub GetSubWithZeroPriority(string userId)
        {
            using (BotContext db = new BotContext())
            {
                db.SaveChanges();
                User user = db.Users.FirstOrDefault(user => user.UserId == userId);
                if (user == null)
                    throw new ArgumentException($"User {userId} doesn't exist");
                var sub = db
                    .UsersSubscriptions
                    .FirstOrDefault(sub => sub.UserId == userId && sub.Priority == 0);
                if (sub == null)
                    return null;
                return new SingleSub(sub);
            }
        }
        public static bool CheckUser(string userId)
        {
            using(BotContext db = new BotContext())
            {
                return db.Users.Any(user => user.UserId == userId);
            }
        }

        //добавляет нового пользователя
        public static void AddUser(string userId)
        {
            using (BotContext db = new BotContext())
            {
                if (db.Users.Any(user => user.UserId == userId))
                    throw new ArgumentException($"User {userId} already exists");
                var newUser = new User(userId);
                db.Users.Add(newUser);
                db.SaveChanges();
            }
        }
        public static bool DoesSubExist(string userId, string sub, string subKey)
        {
            using (BotContext db = new BotContext())
            {
                return db.UsersSubscriptions.Any(
                            user => user.UserId == userId &&
                            user.Subscription == sub &&
                            user.SubscriptionKey == subKey);
            }
        }
        //добавляет новую подписку
        public static void AddSub(string userId, string sub, string subKey)
        {
            using (BotContext db = new BotContext())
            {
                if (db.UsersSubscriptions.Any(
                        user => user.UserId == userId &&
                        user.Subscription == sub &&
                        user.SubscriptionKey == subKey))
                    throw new ArgumentException("Subscription already exists");
                db.UsersSubscriptions.Add(new UserSubscription(userId, sub, subKey,
                    GetUserSubCount(userId)));
                db.SaveChanges();

            }
        }
        
        //убирает подписку
        public static void RemoveSub(string userId, string sub, string subKey)
        {
            using (BotContext db = new BotContext())
            {
                var deletedSub = db.UsersSubscriptions.Where(x =>
                    x.UserId == userId && x.Subscription == sub && x.SubscriptionKey == subKey).FirstOrDefault();
                if (deletedSub == null)
                    throw new ArgumentException("Subscription doesn't exists");
                db.UsersSubscriptions.Remove(deletedSub);
                foreach(var c in db.UsersSubscriptions
                    .Where(x => x.UserId == userId && 
                    x.Priority > deletedSub.Priority))
                {
                        c.Priority--;
                }
                db.SaveChanges();
            }
        }
        
        //Убирает пользователя
        public static void RemoveUser(string userId)
        {
            using (BotContext db = new BotContext())
            {
                var user = db.Users.Where(x => x.UserId == userId).First();
                if (user == null)
                    throw new ArgumentException($"User {userId} doesn't exist");
                db.Users.Remove(user);
                db.UsersSubscriptions.RemoveRange(db.UsersSubscriptions.Where(x => x.UserId == userId));
                db.UsersSeenContent.RemoveRange(db.UsersSeenContent.Where(x => x.UserId == userId));
                db.SaveChanges();
            }
        }


        //Видел ли уже пользователь этот контент
        public static bool HaveUserSeenContent(string userId, string contentId)
        {
            using (BotContext db = new BotContext())
            {
                return db.UsersSeenContent.Any(a => a.UserId == userId && a.ContentId == contentId);
            }
        }

        //добавляет контент в список прочитанных для данного пользователя
        public static void AddSeenContent(string userId, string contentId)
        {
            using(BotContext db = new BotContext())
            {
                if (!db.Users.Any(x => x.UserId == userId))
                    throw new ArgumentException($"User {userId} doesn't exist");
                if (HaveUserSeenContent(userId, contentId))
                    return;
                db.UsersSeenContent.Add(new UserSeenContent(userId, contentId));
                db.SaveChanges();
            }
        }
        
        //циклично сдвигает приоритет подписок пользователя
        public static void ShiftPriority(string userId)
        {
            using (BotContext db = new BotContext())
            {
                if (!db.Users.Any(x => x.UserId == userId))
                    throw new ArgumentException($"User {userId} doesn't exist");
                var subs = db.UsersSubscriptions.Where(x => x.UserId == userId).ToList();
                foreach (var sub in subs)
                {
                    sub.Priority = (sub.Priority + 1) % subs.Count();
                }
                db.SaveChanges();
            }
        }

        public static void CleanReadPosts()
        {
            using (BotContext db = new BotContext())
            {
                db.UsersSeenContent.RemoveRange(db.UsersSeenContent);
                db.SaveChanges();
            }
        }
    }
}