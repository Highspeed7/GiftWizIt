using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class SettingTagNameColumnToVarcharForTagsDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tag_name",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "tag_name",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
