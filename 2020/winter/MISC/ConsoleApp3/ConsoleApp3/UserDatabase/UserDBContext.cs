using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Server.UserDatabase
{
    class UserDBContext : DbContext
    {
        private readonly string Host = "localhost";
        private readonly string Port = "5432";
        private readonly string ThisDatabase = "ServerDb";
        private readonly string Username = "postgres";
        private readonly string Password = "ja2min31";
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<Links> LinkLibrary { get; set; }
        public UserDBContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().HasKey(a => a.EMail);
            modelBuilder.Entity<UserInfo>().HasAlternateKey(a => a.NickName);
            modelBuilder.Entity<UserInfo>().Property(a => a.IsLoggedIn).HasDefaultValue(true);
            modelBuilder.Entity<Links>().HasKey(a => a.Link);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql
                (
                "Host=" + Host + ";" +
                "Port=" + Port + ";" +
                "Database=" + ThisDatabase + ";" +
                "Username=" + Username + ";" +
                "Password=" + Password + ";"
                );
        }
    }
}
