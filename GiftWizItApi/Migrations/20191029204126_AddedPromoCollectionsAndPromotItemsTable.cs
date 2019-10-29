using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddedPromoCollectionsAndPromotItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promo_Collections",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    start_date = table.Column<DateTime>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promo_Collections", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Promo_Items",
                columns: table => new
                {
                    item_id = table.Column<int>(nullable: false),
                    collection_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promo_Items", x => new { x.item_id, x.collection_id });
                    table.ForeignKey(
                        name: "FK_Promo_Items_Promo_Collections_collection_id",
                        column: x => x.collection_id,
                        principalTable: "Promo_Collections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Promo_Items_Items_item_id",
                        column: x => x.item_id,
                        principalTable: "Items",
                        principalColumn: "item_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promo_Items_collection_id",
                table: "Promo_Items",
                column: "collection_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promo_Items");

            migrationBuilder.DropTable(
                name: "Promo_Collections");
        }
    }
}
