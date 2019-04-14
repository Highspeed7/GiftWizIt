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
    [Migration("20190414042155_AddWishListItemsManyToManyRel")]
    partial class AddWishListItemsManyToManyRel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GiftWizItApi.Models.GiftItem", b =>
                {
                    b.Property<int>("GListId")
                        .HasColumnName("g_list_id");

                    b.Property<int>("Item_Id")
                        .HasColumnName("item_id");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

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
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(50);

                    b.Property<string>("UPC")
                        .HasColumnName("upc");

                    b.HasKey("Item_Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("GiftWizItApi.Models.WishItem", b =>
                {
                    b.Property<int>("WListId")
                        .HasColumnName("w_list_id");

                    b.Property<int>("ItemId")
                        .HasColumnName("item_id");

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

                    b.HasKey("Id");

                    b.ToTable("WishLists");
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
#pragma warning restore 612, 618
        }
    }
}
