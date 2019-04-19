using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddAllOtherTablesAndConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_on",
                table: "GiftLists",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    contact_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    email = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.contact_id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    item_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    upc = table.Column<string>(nullable: true),
                    image = table.Column<string>(nullable: true),
                    created_on = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.item_id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    PartnerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.PartnerId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    wish_list_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    created_on = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => x.wish_list_id);
                });

            migrationBuilder.CreateTable(
                name: "GList_Items",
                columns: table => new
                {
                    g_list_id = table.Column<int>(nullable: false),
                    item_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GList_Items", x => new { x.g_list_id, x.item_id });
                    table.ForeignKey(
                        name: "FK_GList_Items_GiftLists_g_list_id",
                        column: x => x.g_list_id,
                        principalTable: "GiftLists",
                        principalColumn: "gift_list_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GList_Items_Items_item_id",
                        column: x => x.item_id,
                        principalTable: "Items",
                        principalColumn: "item_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Links_Items_Partners",
                columns: table => new
                {
                    afflt_link = table.Column<string>(nullable: false),
                    item_id = table.Column<int>(nullable: false),
                    partner_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links_Items_Partners", x => x.afflt_link);
                    table.ForeignKey(
                        name: "FK_Links_Items_Partners_Items_item_id",
                        column: x => x.item_id,
                        principalTable: "Items",
                        principalColumn: "item_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Links_Items_Partners_Partners_partner_id",
                        column: x => x.partner_id,
                        principalTable: "Partners",
                        principalColumn: "PartnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactUsers",
                columns: table => new
                {
                    contact_id = table.Column<int>(nullable: false),
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsers", x => new { x.user_id, x.contact_id });
                    table.ForeignKey(
                        name: "FK_ContactUsers_Contacts_contact_id",
                        column: x => x.contact_id,
                        principalTable: "Contacts",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactUsers_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WList_Items",
                columns: table => new
                {
                    item_id = table.Column<int>(nullable: false),
                    w_list_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WList_Items", x => new { x.w_list_id, x.item_id });
                    table.ForeignKey(
                        name: "FK_WList_Items_Items_item_id",
                        column: x => x.item_id,
                        principalTable: "Items",
                        principalColumn: "item_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WList_Items_WishLists_w_list_id",
                        column: x => x.w_list_id,
                        principalTable: "WishLists",
                        principalColumn: "wish_list_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactUsers_contact_id",
                table: "ContactUsers",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_GList_Items_item_id",
                table: "GList_Items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_Links_Items_Partners_item_id",
                table: "Links_Items_Partners",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_Links_Items_Partners_partner_id",
                table: "Links_Items_Partners",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_WList_Items_item_id",
                table: "WList_Items",
                column: "item_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUsers");

            migrationBuilder.DropTable(
                name: "GList_Items");

            migrationBuilder.DropTable(
                name: "Links_Items_Partners");

            migrationBuilder.DropTable(
                name: "WList_Items");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "WishLists");

            migrationBuilder.DropColumn(
                name: "created_on",
                table: "GiftLists");
        }
    }
}
