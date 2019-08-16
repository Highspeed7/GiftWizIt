using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class RemovePasswordFromSharedListsAddPasswordToGiftLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "Shared_Lists");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "GiftLists",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "GiftLists");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Shared_Lists",
                nullable: true);
        }
    }
}
