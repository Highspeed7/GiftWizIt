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
        public DbSet<SharedLists> SharedLists { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<UserFacebook> UserFacebook { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<ListMessages> ListMessages { get; set; }

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
                .Property(gl => gl.EventDate)
                .HasColumnName("event_date");
            modelBuilder.Entity<GiftLists>()
                .Property(gl => gl.Password)
                .HasColumnName("password")
                .HasDefaultValue("")
                .IsRequired(true);
            modelBuilder.Entity<GiftLists>()
                .Property(gl => gl.UserId)
                .HasColumnName("user_id");
            modelBuilder.Entity<GiftLists>()
                .Property(gl => gl.Deleted)
                .IsRequired(true)
                .HasDefaultValue(false)
                .HasColumnName("_deleted");
            modelBuilder.Entity<GiftLists>()
                .Property(gl => gl.IsPublic)
                .HasColumnName("is_public")
                .HasDefaultValue(false);
            modelBuilder.Entity<GiftLists>()
                .Property(gl => gl.RestrictChat)
                .HasColumnName("restrict_chat")
                .HasDefaultValue(false);
            modelBuilder.Entity<GiftLists>()
                .Property(gl => gl.AllowItemAdds)
                .HasColumnName("allow_item_adds")
                .HasDefaultValue(true);
            modelBuilder.Entity<GiftLists>()
                .HasOne(gl => gl.Users)
                .WithMany(u => u.GiftLists)
                .HasForeignKey(gl => gl.UserId);

            // ListMessages Configuration
            modelBuilder.Entity<ListMessages>()
                .ToTable("GList_Messages")
                .HasKey(lm => lm.Id);
            modelBuilder.Entity<ListMessages>()
                .Property(lm => lm.Id)
                .HasColumnName("id");
            modelBuilder.Entity<ListMessages>()
                .Property(lm => lm.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<ListMessages>()
                .Property(lm => lm.UserId)
                .HasColumnName("user_id")
                .IsRequired(true);
            modelBuilder.Entity<ListMessages>()
                .Property(lm => lm.GiftListId)
                .HasColumnName("gift_list_id");
            modelBuilder.Entity<ListMessages>()
                .Property(lm => lm.Message)
                .IsRequired(true)
                .HasColumnName("message");
            modelBuilder.Entity<ListMessages>()
                .HasOne(lm => lm.User)
                .WithMany(u => u.ListMessages)
                .HasForeignKey(lm => lm.UserId);
            modelBuilder.Entity<ListMessages>()
                .HasOne(lm => lm.GiftList)
                .WithMany(gl => gl.ListMessages)
                .HasForeignKey(lm => lm.GiftListId);

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
            modelBuilder.Entity<Items>()
                .Property(i => i.ProductId)
                .HasColumnName("product_id")
                .IsRequired(false);

            //GiftList-Items Linking table configuration
            modelBuilder.Entity<GiftItem>()
                .ToTable("GList_Items")
                .HasKey(gi => new { gi.GListId, gi.Item_Id });
            modelBuilder.Entity<GiftItem>()
                .Property(gi => gi.GListId)
                .HasColumnName("g_list_id");
            modelBuilder.Entity<GiftItem>()
                .Property(gi => gi.Purchase_Status)
                .HasColumnName("purchase_status")
                .HasMaxLength(5);
            modelBuilder.Entity<GiftItem>()
                .Property(gi => gi.Item_Id)
                .HasColumnName("item_id");
            modelBuilder.Entity<GiftItem>()
                .Property(gi => gi.Deleted)
                .HasColumnName("_deleted")
                .HasDefaultValue(false)
                .IsRequired(true);
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
            modelBuilder.Entity<Users>()
                .Property(u => u.Email)
                .HasColumnName("email")
                .IsRequired(true);
            modelBuilder.Entity<Users>()
                .HasAlternateKey(u => u.Email)
                .HasName("email");
            modelBuilder.Entity<Users>()
                .Property(u => u.Name)
                .HasColumnName("name")
                .IsRequired(true);

            // User Checkout
            modelBuilder.Entity<UserCheckout>()
                .HasKey(uc => new { uc.UserId, uc.CheckoutId });
            modelBuilder.Entity<UserCheckout>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCheckouts)
                .HasForeignKey(uc => uc.UserId);
            modelBuilder.Entity<UserCheckout>()
                .Property(uc => uc.UserId)
                .HasColumnName("user_id");
            modelBuilder.Entity<UserCheckout>()
                .Property(uc => uc.CheckoutId)
                .HasColumnName("checkout_id");
            modelBuilder.Entity<UserCheckout>()
                .Property(uc => uc.DateCreated)
                .IsRequired(true)
                .HasColumnName("date_created");
            modelBuilder.Entity<UserCheckout>()
                .Property(uc => uc.DateCompleted)
                .HasColumnName("date_completed");
            modelBuilder.Entity<UserCheckout>()
                .Property(uc => uc.Completed)
                .IsRequired(true)
                .HasDefaultValue(false)
                .HasColumnName("completed");
            modelBuilder.Entity<UserCheckout>()
                .Property(uc => uc.WebUrl)
                .HasColumnName("web_url");

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
                .Property(c => c.Verified)
                .HasColumnName("verified")
                .HasDefaultValue(false);
            modelBuilder.Entity<Contacts>()
                .Property(c => c.EmailSent)
                .HasColumnName("email_sent")
                .HasDefaultValue(false);
            modelBuilder.Entity<Contacts>()
                .Property(c => c.VerifyGuid)
                .HasColumnName("verify_guid")
                .IsRequired(false);
            modelBuilder.Entity<Contacts>()
                .Property(c => c.UserId)
                .IsRequired(false);

            // ContactUsers Linking table configuration
            modelBuilder.Entity<ContactUsers>()
                .HasKey(cu => new { cu.UserId, cu.ContactId });
            modelBuilder.Entity<ContactUsers>()
                .Property(cu => cu.ContactId)
                .HasColumnName("contact_id");
            modelBuilder.Entity<ContactUsers>()
                .Property(cu => cu.ContactAlias)
                .HasColumnName("contact_alias")
                .IsRequired(true)
                .HasDefaultValue("Contact");
            modelBuilder.Entity<ContactUsers>()
                .Property(cu => cu.UserId)
                .HasColumnName("user_id");
            modelBuilder.Entity<ContactUsers>()
                .Property(cu => cu.Deleted)
                .HasColumnName("_deleted")
                .IsRequired(true)
                .HasDefaultValue(false);
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

            // Shared-Lists linking table configuration
            modelBuilder.Entity<SharedLists>()
                .ToTable("Shared_Lists")
                .HasKey(sl => sl.SharedListId);
            modelBuilder.Entity<SharedLists>()
                .Property(sl => sl.SharedListId)
                .HasColumnName("shared_list_id");
            modelBuilder.Entity<SharedLists>()
                .Property(sl => sl.EmailSent)
                .HasColumnName("email_sent")
                .HasDefaultValue(false);
            modelBuilder.Entity<SharedLists>()
                .Property(sl => sl.ContactId)
                .HasColumnName("contact_id");
            modelBuilder.Entity<SharedLists>()
                .Property(sl => sl.UserId)
                .HasColumnName("user_id");
            modelBuilder.Entity<SharedLists>()
                .Property(sl => sl.GiftListId)
                .HasColumnName("g_list_id");
            modelBuilder.Entity<SharedLists>()
                .HasOne(sl => sl.Contact)
                .WithMany(c => c.SharedLists)
                .HasForeignKey(sl => sl.ContactId);
            modelBuilder.Entity<SharedLists>()
                .HasOne(sl => sl.GiftList)
                .WithMany(gl => gl.SharedLists)
                .HasForeignKey(sl => sl.GiftListId);
            modelBuilder.Entity<SharedLists>()
                .HasOne(sl => sl.User)
                .WithMany(u => u.SharedLists)
                .HasForeignKey(sl => sl.UserId);

            // Favorites table configuration
            modelBuilder.Entity<Favorites>()
                .HasKey(f => f.Id);
            modelBuilder.Entity<Favorites>()
                .Property(f => f.Id)
                .HasColumnName("id");
            modelBuilder.Entity<Favorites>()
                .Property(f => f.G_List_Id)
                .HasColumnName("g_list_id");
            modelBuilder.Entity<Favorites>()
                .Property(f => f.Item_Id)
                .HasColumnName("item_id");
            modelBuilder.Entity<Favorites>()
                .Property(f => f.Contact_Id)
                .HasColumnName("contact_id");
            modelBuilder.Entity<Favorites>()
                .HasOne(f => f.Item)
                .WithMany(i => i.Favorites)
                .HasForeignKey(f => f.Item_Id);
            modelBuilder.Entity<Favorites>()
                .HasOne(f => f.Contact)
                .WithMany(c => c.Favorites)
                .HasForeignKey(f => f.Contact_Id);
            modelBuilder.Entity<Favorites>()
                .HasOne(f => f.GiftList)
                .WithMany(gl => gl.Favorites)
                .HasForeignKey(f => f.G_List_Id);

            // Notifications table config
            modelBuilder.Entity<Notifications>()
                .HasKey(n => n.Id);
            modelBuilder.Entity<Notifications>()
                .Property(n => n.Id)
                .HasColumnName("id");
            modelBuilder.Entity<Notifications>()
                .Property(n => n.UserId)
                .HasColumnName("user_id")
                .IsRequired(false);
            modelBuilder.Entity<Notifications>()
                .Property(n => n.Type)
                .HasColumnName("type")
                .HasMaxLength(50)
                .IsRequired(true);
            modelBuilder.Entity<Notifications>()
                .Property(n => n.Title)
                .HasColumnName("title");
            modelBuilder.Entity<Notifications>()
                .Property(n => n.Message)
                .HasColumnName("message")
                .IsRequired(true)
                .HasMaxLength(250);
            modelBuilder.Entity<Notifications>()
                .Property(n => n.CreatedOn)
                .HasColumnName("created_on")
                .IsRequired(true);
            modelBuilder.Entity<Notifications>()
                .Property(n => n.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);
            modelBuilder.Entity<Notifications>()
                .Property(n => n.Dismissed)
                .HasColumnName("dismissed")
                .HasDefaultValue(false);
            modelBuilder.Entity<Notifications>()
                .Property(n => n.Persist)
                .HasColumnName("persist")
                .HasDefaultValue(false);
            modelBuilder.Entity<Notifications>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);

            // UsersFacebook Associate table config
            modelBuilder.Entity<UserFacebook>()
                .ToTable("User_Facebook_Assoc")
                .HasKey(uf => new { uf.UserId, uf.FacebookId });
            modelBuilder.Entity<UserFacebook>()
                .Property(uf => uf.UserId)
                .HasColumnName("user_id")
                .IsRequired(true);
            modelBuilder.Entity<UserFacebook>()
                .Property(uf => uf.FacebookId)
                .HasColumnName("facebook_id")
                .HasMaxLength(50)
                .IsRequired(true);
            modelBuilder.Entity<UserFacebook>()
                .HasOne(uf => uf.User)
                .WithOne(uf => uf.UserFacebook);
        }
    }
}
