using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<GiftLists> GiftLists { get; set; }
        public DbSet<WishLists> WishLists { get; set; }
        public DbSet<Items> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // GiftLists Configuration
            modelBuilder.Entity<GiftLists>().Property(gl => gl.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<GiftLists>().Property(gl => gl.CreatedAt).ValueGeneratedOnAdd();

            // WishLists Configuration
            modelBuilder.Entity<WishLists>().Property(wl => wl.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<WishLists>().Property(wl => wl.CreatedAt).ValueGeneratedOnAdd();

            // Items Configuration
            modelBuilder.Entity<Items>().HasKey(i => i.Item_Id);
            modelBuilder.Entity<Items>().Property(i => i.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<Items>().Property(i => i.CreatedOn).ValueGeneratedOnAdd();
        }
    }
}
