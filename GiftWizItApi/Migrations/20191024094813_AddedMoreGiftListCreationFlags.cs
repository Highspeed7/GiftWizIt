using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddedMoreGiftListCreationFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "allow_item_adds",
                table: "GiftLists",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "restrict_chat",
                table: "GiftLists",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "allow_item_adds",
                table: "GiftLists");

            migrationBuilder.DropColumn(
                name: "restrict_chat",
                table: "GiftLists");
        }
    }
}
