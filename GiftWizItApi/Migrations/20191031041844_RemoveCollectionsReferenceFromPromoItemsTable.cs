using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class RemoveCollectionsReferenceFromPromoItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promo_Items_Promo_Collections_collection_id",
                table: "Promo_Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promo_Items",
                table: "Promo_Items");

            migrationBuilder.DropIndex(
                name: "IX_Promo_Items_collection_id",
                table: "Promo_Items");

            migrationBuilder.DropColumn(
                name: "collection_id",
                table: "Promo_Items");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Promo_Items",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "PromoCollectionsId",
                table: "Promo_Items",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promo_Items",
                table: "Promo_Items",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Promo_Items_item_id",
                table: "Promo_Items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_Promo_Items_PromoCollectionsId",
                table: "Promo_Items",
                column: "PromoCollectionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promo_Items_Promo_Collections_PromoCollectionsId",
                table: "Promo_Items",
                column: "PromoCollectionsId",
                principalTable: "Promo_Collections",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promo_Items_Promo_Collections_PromoCollectionsId",
                table: "Promo_Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promo_Items",
                table: "Promo_Items");

            migrationBuilder.DropIndex(
                name: "IX_Promo_Items_item_id",
                table: "Promo_Items");

            migrationBuilder.DropIndex(
                name: "IX_Promo_Items_PromoCollectionsId",
                table: "Promo_Items");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Promo_Items");

            migrationBuilder.DropColumn(
                name: "PromoCollectionsId",
                table: "Promo_Items");

            migrationBuilder.AddColumn<int>(
                name: "collection_id",
                table: "Promo_Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promo_Items",
                table: "Promo_Items",
                columns: new[] { "item_id", "collection_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Promo_Items_collection_id",
                table: "Promo_Items",
                column: "collection_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Promo_Items_Promo_Collections_collection_id",
                table: "Promo_Items",
                column: "collection_id",
                principalTable: "Promo_Collections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
