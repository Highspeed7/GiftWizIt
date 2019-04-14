using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddWishListItemsManyToManyRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_WList_Items_item_id",
                table: "WList_Items",
                column: "item_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WList_Items");
        }
    }
}
