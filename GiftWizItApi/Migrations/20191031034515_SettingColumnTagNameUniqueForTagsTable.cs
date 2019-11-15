using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class SettingColumnTagNameUniqueForTagsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tag_name",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Tags_tag_name",
                table: "Tags",
                column: "tag_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tags_tag_name",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "tag_name",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
