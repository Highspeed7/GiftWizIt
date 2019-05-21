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
        public DbSet<WishItem> WishItems { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Partners> Partners { get; set; }
        public DbSet<ContactUsers> ContactUsers { get; set; }
        public DbSet<LnksItmsPtnrs> LinkItemsPartners { get; set; }
        public DbQuery<WishListRaw> DbWishListObject { get; set; }
        public DbQuery<CombGiftItems> DbGiftItemsObject { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // GiftLists Configuration
            modelBuilder.Entity<GiftLists>()
                .Property(gl => gl.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<GiftLists>()
                .Property(gl => gl.CreatedAt)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<GiftLists>()
                .Property(gl => gl.UserId)
                .HasColumnName("user_id");
            modelBuilder.Entity<GiftLists>()
                .Property(gl => gl.Deleted)
                .IsRequired(true)
                .HasDefaultValue(false)
                .HasColumnName("_deleted");
            modelBuilder.Entity<GiftLists>()
                .HasOne(gl => gl.Users)
                .WithMany(u => u.GiftLists)
                .HasForeignKey(gl => gl.UserId);
                

            // WishLists Configuration
            modelBuilder.Entity<WishLists>()
                .Property(wl => wl.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            modelBuilder
                .Entity<WishLists>()
                .Property(wl => wl.CreatedAt)
                .ValueGeneratedOnAdd();
            modelBuilder
                .Entity<WishLists>()
                .HasOne(wl => wl.Users)
                .WithMany(u => u.WishLists)
                .HasForeignKey(wl => wl.UserId);

            // Items Configuration
            modelBuilder.Entity<Items>().HasKey(i => i.Item_Id);
            modelBuilder.Entity<Items>().Property(i => i.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<Items>()
                .Property(i => i.CreatedOn)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Items>()
                .Property(i => i.Name)
                .HasMaxLength(250)
                .IsRequired(true);
            modelBuilder.Entity<Items>()
                .Property(i => i.Image)
                .HasMaxLength(450);

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
               .Property(wi => wi.Deleted)
               .IsRequired(true)
               .HasDefaultValue(false)
               .HasColumnName("_deleted");
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

            // Users Configuration
            modelBuilder.Entity<Users>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<Users>()
                .Property(u => u.UserId)
                .HasColumnName("user_id")
                .IsRequired(true);

            // Partners Configuration
            modelBuilder.Entity<Partners>()
                .HasKey(p => p.PartnerId);
            modelBuilder.Entity<Partners>()
                .Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired(true);

            // Contact Configuration
            modelBuilder.Entity<Contacts>()
                .HasKey(c => c.ContactId);
            modelBuilder.Entity<Contacts>()
                .Property(c => c.ContactId)
                .HasColumnName("contact_id");
            modelBuilder.Entity<Contacts>()
                .Property(c => c.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired(true);
            modelBuilder.Entity<Contacts>()
                .Property(c => c.Email)
                .HasColumnName("email")
                .HasMaxLength(100)
                .IsRequired(true);
            modelBuilder.Entity<Contacts>()
                .Property(c => c.AccessGuid)
                .HasColumnName("access_guid")
                .ValueGeneratedOnAdd()
                .IsRequired(true);
            modelBuilder.Entity<Contacts>()
                .Property(c => c.Verified)
                .HasColumnName("verified")
                .HasDefaultValue(false);
            modelBuilder.Entity<Contacts>()
                .Property(c => c.EmailSent)
                .HasColumnName("email_sent")
                .HasDefaultValue(false);

            // ContactUsers Linking table configuration
            modelBuilder.Entity<ContactUsers>()
                .HasKey(cu => new { cu.UserId, cu.ContactId });
            modelBuilder.Entity<ContactUsers>()
                .Property(cu => cu.ContactId)
                .HasColumnName("contact_id");
            modelBuilder.Entity<ContactUsers>()
                .Property(cu => cu.UserId)
                .HasColumnName("user_id");
            modelBuilder.Entity<ContactUsers>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.ContactUsers)
                .HasForeignKey(cu => cu.UserId);
            modelBuilder.Entity<ContactUsers>()
                .HasOne(cu => cu.Contact)
                .WithMany(c => c.ContactUsers)
                .HasForeignKey(cu => cu.ContactId);

            // Links-Items-Partners Linking table configuration
            modelBuilder.Entity<LnksItmsPtnrs>()
                .ToTable("Links_Items_Partners")
                .HasKey(lip => new { lip.ItemId, lip.PartnerId });
            modelBuilder.Entity<LnksItmsPtnrs>()
                .Property(lip => lip.AffliateLink)
                .HasColumnName("afflt_link");
            modelBuilder.Entity<LnksItmsPtnrs>()
                .Property(lip => lip.ItemId)
                .HasColumnName("item_id");
            modelBuilder.Entity<LnksItmsPtnrs>()
                .Property(lip => lip.PartnerId)
                .HasColumnName("partner_id");
            modelBuilder.Entity<LnksItmsPtnrs>()
                .HasOne(lip => lip.Item)
                .WithMany(i => i.LinkItemPartners)
                .HasForeignKey(lip => lip.ItemId);
            modelBuilder.Entity<LnksItmsPtnrs>()
                .HasOne(lip => lip.Partner)
                .WithMany(p => p.LinkItemPartners)
                .HasForeignKey(lip => lip.PartnerId);
        }
    }
}
