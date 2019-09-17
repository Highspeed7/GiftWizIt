﻿// <auto-generated />
using System;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GiftWizItApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190917034942_AddedContactAliasToContactUsersTable")]
    partial class AddedContactAliasToContactUsersTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GiftWizItApi.Models.ContactUsers", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnName("user_id");

                    b.Property<int>("ContactId")
                        .HasColumnName("contact_id");

                    b.Property<string>("ContactAlias")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("contact_alias")
                        .HasDefaultValue("Contact");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("_deleted")
                        .HasDefaultValue(false);

                    b.HasKey("UserId", "ContactId");

                    b.HasIndex("ContactId");

                    b.ToTable("ContactUsers");
                });

            modelBuilder.Entity("GiftWizItApi.Models.Contacts", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("contact_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasMaxLength(100);

                    b.Property<bool>("EmailSent")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("email_sent")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(100);

                    b.Property<string>("UserId");

                    b.Property<bool>("Verified")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("verified")
                        .HasDefaultValue(false);

                    b.Property<string>("VerifyGuid")
                        .HasColumnName("verify_guid");

                    b.HasKey("ContactId");

                    b.HasIndex("UserId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("GiftWizItApi.Models.Favorites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Contact_Id")
                        .HasColumnName("contact_id");

                    b.Property<int>("G_List_Id")
                        .HasColumnName("g_list_id");

                    b.Property<int>("Item_Id")
                        .HasColumnName("item_id");

                    b.HasKey("Id");

                    b.HasIndex("Contact_Id");

                    b.HasIndex("G_List_Id");

                    b.HasIndex("Item_Id");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("GiftWizItApi.Models.GiftItem", b =>
                {
                    b.Property<int>("GListId")
                        .HasColumnName("g_list_id");

                    b.Property<int>("Item_Id")
                        .HasColumnName("item_id");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("_deleted")
                        .HasDefaultValue(false);

                    b.Property<string>("Purchase_Status")
                        .HasColumnName("purchase_status")
                        .HasMaxLength(5);

                    b.HasKey("GListId", "Item_Id");

                    b.HasIndex("Item_Id");

                    b.ToTable("GList_Items");
                });

            modelBuilder.Entity("GiftWizItApi.Models.GiftLists", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("gift_list_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("_deleted")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("EventDate")
                        .HasColumnName("event_date");

                    b.Property<bool>("IsPublic")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("is_public")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("password")
                        .HasDefaultValue("");

                    b.Property<string>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("GiftLists");
                });

            modelBuilder.Entity("GiftWizItApi.Models.Items", b =>
                {
                    b.Property<int>("Item_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("item_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Image")
                        .HasColumnName("image")
                        .HasMaxLength(450);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(250);

                    b.Property<string>("UPC")
                        .HasColumnName("upc");

                    b.HasKey("Item_Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("GiftWizItApi.Models.LnksItmsPtnrs", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnName("item_id");

                    b.Property<int>("PartnerId")
                        .HasColumnName("partner_id");

                    b.Property<string>("AffliateLink")
                        .HasColumnName("afflt_link");

                    b.HasKey("ItemId", "PartnerId");

                    b.HasIndex("PartnerId");

                    b.ToTable("Links_Items_Partners");
                });

            modelBuilder.Entity("GiftWizItApi.Models.Notifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("deleted")
                        .HasDefaultValue(false);

                    b.Property<bool>("Dismissed")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("dismissed")
                        .HasDefaultValue(false);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnName("message")
                        .HasMaxLength(250);

                    b.Property<bool>("Persist")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("persist")
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .HasColumnName("title");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("type")
                        .HasMaxLength(50);

                    b.Property<string>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("GiftWizItApi.Models.Partners", b =>
                {
                    b.Property<int>("PartnerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Domain");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(100);

                    b.HasKey("PartnerId");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("GiftWizItApi.Models.SharedLists", b =>
                {
                    b.Property<int>("SharedListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("shared_list_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContactId")
                        .HasColumnName("contact_id");

                    b.Property<bool>("EmailSent")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("email_sent")
                        .HasDefaultValue(false);

                    b.Property<int>("GiftListId")
                        .HasColumnName("g_list_id");

                    b.Property<string>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("SharedListId");

                    b.HasIndex("ContactId");

                    b.HasIndex("GiftListId");

                    b.HasIndex("UserId");

                    b.ToTable("Shared_Lists");
                });

            modelBuilder.Entity("GiftWizItApi.Models.UserFacebook", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnName("user_id");

                    b.Property<string>("FacebookId")
                        .HasColumnName("facebook_id")
                        .HasMaxLength(50);

                    b.HasKey("UserId", "FacebookId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("User_Facebook_Assoc");
                });

            modelBuilder.Entity("GiftWizItApi.Models.Users", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.HasKey("UserId");

                    b.HasAlternateKey("Email")
                        .HasName("email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GiftWizItApi.Models.WishItem", b =>
                {
                    b.Property<int>("WListId")
                        .HasColumnName("w_list_id");

                    b.Property<int>("ItemId")
                        .HasColumnName("item_id");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("_deleted")
                        .HasDefaultValue(false);

                    b.HasKey("WListId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("WList_Items");
                });

            modelBuilder.Entity("GiftWizItApi.Models.WishLists", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("wish_list_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(50);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("WishLists");
                });

            modelBuilder.Entity("GiftWizItApi.Models.ContactUsers", b =>
                {
                    b.HasOne("GiftWizItApi.Models.Contacts", "Contact")
                        .WithMany("ContactUsers")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GiftWizItApi.Models.Users", "User")
                        .WithMany("ContactUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GiftWizItApi.Models.Contacts", b =>
                {
                    b.HasOne("GiftWizItApi.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GiftWizItApi.Models.Favorites", b =>
                {
                    b.HasOne("GiftWizItApi.Models.Contacts", "Contact")
                        .WithMany("Favorites")
                        .HasForeignKey("Contact_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GiftWizItApi.Models.GiftLists", "GiftList")
                        .WithMany("Favorites")
                        .HasForeignKey("G_List_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GiftWizItApi.Models.Items", "Item")
                        .WithMany("Favorites")
                        .HasForeignKey("Item_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GiftWizItApi.Models.GiftItem", b =>
                {
                    b.HasOne("GiftWizItApi.Models.GiftLists", "GiftList")
                        .WithMany("GiftItems")
                        .HasForeignKey("GListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GiftWizItApi.Models.Items", "Item")
                        .WithMany("GiftItems")
                        .HasForeignKey("Item_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GiftWizItApi.Models.GiftLists", b =>
                {
                    b.HasOne("GiftWizItApi.Models.Users", "Users")
                        .WithMany("GiftLists")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GiftWizItApi.Models.LnksItmsPtnrs", b =>
                {
                    b.HasOne("GiftWizItApi.Models.Items", "Item")
                        .WithMany("LinkItemPartners")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GiftWizItApi.Models.Partners", "Partner")
                        .WithMany("LinkItemPartners")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GiftWizItApi.Models.Notifications", b =>
                {
                    b.HasOne("GiftWizItApi.Models.Users", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GiftWizItApi.Models.SharedLists", b =>
                {
                    b.HasOne("GiftWizItApi.Models.Contacts", "Contact")
                        .WithMany("SharedLists")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GiftWizItApi.Models.GiftLists", "GiftList")
                        .WithMany("SharedLists")
                        .HasForeignKey("GiftListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GiftWizItApi.Models.Users", "User")
                        .WithMany("SharedLists")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GiftWizItApi.Models.UserFacebook", b =>
                {
                    b.HasOne("GiftWizItApi.Models.Users", "User")
                        .WithOne("UserFacebook")
                        .HasForeignKey("GiftWizItApi.Models.UserFacebook", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GiftWizItApi.Models.WishItem", b =>
                {
                    b.HasOne("GiftWizItApi.Models.Items", "Item")
                        .WithMany("WishItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GiftWizItApi.Models.WishLists", "WishList")
                        .WithMany("WishItems")
                        .HasForeignKey("WListId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GiftWizItApi.Models.WishLists", b =>
                {
                    b.HasOne("GiftWizItApi.Models.Users", "Users")
                        .WithMany("WishLists")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
