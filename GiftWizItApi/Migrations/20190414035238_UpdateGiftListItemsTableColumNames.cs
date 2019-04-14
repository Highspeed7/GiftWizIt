using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class UpdateGiftListItemsTableColumNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GList_Items_GiftLists_GListId",
                table: "GList_Items");

            migrationBuilder.DropForeignKey(
                name: "FK_GList_Items_Items_Item_Id",
                table: "GList_Items");

            migrationBuilder.RenameColumn(
                name: "Item_Id",
                table: "GList_Items",
                newName: "item_id");

            migrationBuilder.RenameColumn(
                name: "GListId",
                table: "GList_Items",
                newName: "g_list_id");

            migrationBuilder.RenameIndex(
                name: "IX_GList_Items_Item_Id",
                table: "GList_Items",
                newName: "IX_GList_Items_item_id");

            migrationBuilder.AddForeignKey(
                name: "FK_GList_Items_GiftLists_g_list_id",
                table: "GList_Items",
                column: "g_list_id",
                principalTable: "GiftLists",
                principalColumn: "gift_list_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GList_Items_Items_item_id",
                table: "GList_Items",
                column: "item_id",
                principalTable: "Items",
                principalColumn: "item_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GList_Items_GiftLists_g_list_id",
                table: "GList_Items");

            migrationBuilder.DropForeignKey(
                name: "FK_GList_Items_Items_item_id",
                table: "GList_Items");

            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "GList_Items",
                newName: "Item_Id");

            migrationBuilder.RenameColumn(
                name: "g_list_id",
                table: "GList_Items",
                newName: "GListId");

            migrationBuilder.RenameIndex(
                name: "IX_GList_Items_item_id",
                table: "GList_Items",
                newName: "IX_GList_Items_Item_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GList_Items_GiftLists_GListId",
                table: "GList_Items",
                column: "GListId",
                principalTable: "GiftLists",
                principalColumn: "gift_list_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GList_Items_Items_Item_Id",
                table: "GList_Items",
                column: "Item_Id",
                principalTable: "Items",
                principalColumn: "item_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
