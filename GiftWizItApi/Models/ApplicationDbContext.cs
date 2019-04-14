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
        public DbSet<GiftItem> GiftItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // GiftLists Configuration
            modelBuilder.Entity<GiftLists>()
                .Property(gl => gl.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<GiftLists>()
                .Property(gl => gl.CreatedAt)
                .ValueGeneratedOnAdd();

            // WishLists Configuration
            modelBuilder.Entity<WishLists>()
                .Property(wl => wl.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            modelBuilder
                .Entity<WishLists>()
                .Property(wl => wl.CreatedAt)
                .ValueGeneratedOnAdd();

            // Items Configuration
            modelBuilder.Entity<Items>().HasKey(i => i.Item_Id);
            modelBuilder.Entity<Items>().Property(i => i.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<Items>()
                .Property(i => i.CreatedOn)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Items>()
                .Property(i => i.Name)
                .HasMaxLength(50)
                .IsRequired(true);

            //GiftList-Items Linking table configuration
            modelBuilder.Entity<GiftItem>()
                .ToTable("GList_Items")
                .HasKey(gi => new { gi.GListId, gi.Item_Id });
            modelBuilder.Entity<GiftItem>()
                .Property(gi => gi.GListId)
                .HasColumnName("g_list_id");
            modelBuilder.Entity<GiftItem>()
                .Property(gi => gi.Item_Id)
                .HasColumnName("item_id");
            modelBuilder.Entity<GiftItem>()
                .HasOne(gi => gi.GiftList)
                .WithMany(gl => gl.GiftItems)
                .HasForeignKey(gi => gi.GListId);
            modelBuilder.Entity<GiftItem>()
                .HasOne(gi => gi.Item)
                .WithMany(g => g.GiftItems)
                .HasForeignKey(gi => gi.Item_Id);

            // WishList-Items Linking table configuration
            modelBuilder.Entity<WishItem>()
                .ToTable("WList_Items")
                .HasKey(wi => new { wi.WListId, wi.ItemId });
            modelBuilder.Entity<WishItem>()
                .Property(wi => wi.ItemId)
                .HasColumnName("item_id");
            modelBuilder.Entity<WishItem>()
                .Property(wi => wi.WListId)
                .HasColumnName("w_list_id");
            modelBuilder.Entity<WishItem>()
                .HasOne(wi => wi.Item)
                .WithMany(i => i.WishItems)
                .HasForeignKey(wi => wi.ItemId);
            modelBuilder.Entity<WishItem>()
                .HasOne(wi => wi.WishList)
                .WithMany(wl => wl.WishItems)
                .HasForeignKey(wi => wi.WListId);
        }
    }
}
