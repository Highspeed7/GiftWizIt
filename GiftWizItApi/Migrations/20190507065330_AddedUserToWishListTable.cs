using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddedUserToWishListTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Item_Id",
                table: "WishLists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WishLists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_Item_Id",
                table: "WishLists",
                column: "Item_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_UserId",
                table: "WishLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Items_Item_Id",
                table: "WishLists",
                column: "Item_Id",
                principalTable: "Items",
                principalColumn: "item_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Users_UserId",
                table: "WishLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Items_Item_Id",
                table: "WishLists");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Users_UserId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_Item_Id",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_UserId",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "Item_Id",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WishLists");
        }
    }
}
