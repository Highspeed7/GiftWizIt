using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class DefineGiftListsItemsManyToManyRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GList_Items",
                columns: table => new
                {
                    GListId = table.Column<int>(nullable: false),
                    Item_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GList_Items", x => new { x.GListId, x.Item_Id });
                    table.ForeignKey(
                        name: "FK_GList_Items_GiftLists_GListId",
                        column: x => x.GListId,
                        principalTable: "GiftLists",
                        principalColumn: "gift_list_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GList_Items_Items_Item_Id",
                        column: x => x.Item_Id,
                        principalTable: "Items",
                        principalColumn: "item_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GList_Items_Item_Id",
                table: "GList_Items",
                column: "Item_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GList_Items");
        }
    }
}
