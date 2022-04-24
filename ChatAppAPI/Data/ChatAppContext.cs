using ChatAppAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppAPI.Data
{
    public class ChatAppContext : DbContext
    {
        public ChatAppContext(DbContextOptions<ChatAppContext> options): base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluent API
            modelBuilder.Entity<AppUser>().HasMany(m => m.Messages)
                .WithOne(u => u.AppUser)
                .HasForeignKey(u => u.AppUserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppUser>().HasIndex(s => s.Email).IsUnique();
        }
    }
}
