using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class RemovedLinkFromLnksItmsPtnrsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Links_Items_Partners",
                table: "Links_Items_Partners");

            migrationBuilder.DropIndex(
                name: "IX_Links_Items_Partners_item_id",
                table: "Links_Items_Partners");

            migrationBuilder.DropColumn(
                name: "afflt_link",
                table: "Links_Items_Partners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Links_Items_Partners",
                table: "Links_Items_Partners",
                columns: new[] { "item_id", "partner_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Links_Items_Partners",
                table: "Links_Items_Partners");

            migrationBuilder.AddColumn<string>(
                name: "afflt_link",
                table: "Links_Items_Partners",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Links_Items_Partners",
                table: "Links_Items_Partners",
                column: "afflt_link");

            migrationBuilder.CreateIndex(
                name: "IX_Links_Items_Partners_item_id",
                table: "Links_Items_Partners",
                column: "item_id");
        }
    }
}
