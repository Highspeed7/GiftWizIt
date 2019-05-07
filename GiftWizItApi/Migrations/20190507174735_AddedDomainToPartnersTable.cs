using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddedDomainToPartnersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Items_Item_Id",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_Item_Id",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "Item_Id",
                table: "WishLists");

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "Partners",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domain",
                table: "Partners");

            migrationBuilder.AddColumn<int>(
                name: "Item_Id",
                table: "WishLists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_Item_Id",
                table: "WishLists",
                column: "Item_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Items_Item_Id",
                table: "WishLists",
                column: "Item_Id",
                principalTable: "Items",
                principalColumn: "item_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
