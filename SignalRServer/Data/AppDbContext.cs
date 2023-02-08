using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KoalitionServer.Models;

namespace KoalitionServer.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
            { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>()
                .HasOne<User>(a => a.Sender)
                .WithMany(d => d.Messages)
                .HasForeignKey(a => a.UserID);
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext() => Database.EnsureCreated();
    }
}
