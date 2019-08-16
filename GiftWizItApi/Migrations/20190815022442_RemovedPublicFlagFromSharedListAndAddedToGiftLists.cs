using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class RemovedPublicFlagFromSharedListAndAddedToGiftLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_public",
                table: "Shared_Lists");

            migrationBuilder.AddColumn<bool>(
                name: "is_public",
                table: "GiftLists",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_public",
                table: "GiftLists");

            migrationBuilder.AddColumn<bool>(
                name: "is_public",
                table: "Shared_Lists",
                nullable: false,
                defaultValue: false);
        }
    }
}
