using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
namespace DataBaseWorker
{
    internal class BotContext : DbContext
    {
        readonly static string Host = "reddit-postman-server.postgres.database.azure.com";
        readonly static string Username = "revinss@reddit-postman-server";
        readonly static string Password = "iWS-TiQ-dca-NA3";
        internal DbSet<User> Users { get; set; }
        internal DbSet<UserSubCount> UsersSubs { get; set; }
        internal DbSet<UserReadPosts> UsersPosts { get; set; }
        public BotContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host={Host};Port=5432;Database=BotDb;Username={Username};Password={Password}");
        }
    }
}
