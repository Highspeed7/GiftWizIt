using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddedItemClaimsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item_Claims",
                columns: table => new
                {
                    claim_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    item_id = table.Column<int>(nullable: false),
                    gift_list_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Claims", x => x.claim_id);
                    table.ForeignKey(
                        name: "FK_Item_Claims_GiftLists_gift_list_id",
                        column: x => x.gift_list_id,
                        principalTable: "GiftLists",
                        principalColumn: "gift_list_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Claims_Items_item_id",
                        column: x => x.item_id,
                        principalTable: "Items",
                        principalColumn: "item_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Claims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_Claims_gift_list_id",
                table: "Item_Claims",
                column: "gift_list_id");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Claims_item_id",
                table: "Item_Claims",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Claims_UserId",
                table: "Item_Claims",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item_Claims");
        }
    }
}
