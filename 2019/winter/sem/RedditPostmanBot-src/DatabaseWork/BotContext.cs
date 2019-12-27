using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace DataBaseWorker
{
    internal class BotContext : DbContext
    {
        readonly static string Host = "localhost";
        readonly static string Port = "5432";
        readonly static string Username = "postgres";
        readonly static string Password = "ja2min31";
        internal DbSet<UserSubscription> UsersSubscriptions { get; set; }
        internal DbSet<User> Users { get; set; }
        internal DbSet<UserSeenContent> UsersSeenContent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(o => o.UserId);
            modelBuilder.Entity<User>()
                .HasIndex(o => o.UserId)
                .IsUnique();

            modelBuilder.Entity<UserSubscription>()
            .HasKey(o => new { o.UserId, o.Subscription, o.SubscriptionKey });
            modelBuilder.Entity<UserSeenContent>()
            .HasKey(o => new { o.UserId, o.ContentId });
        }

        public BotContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host={Host};Port={Port};Database=BotDb;Username={Username};Password={Password}");
        }
    }
}
