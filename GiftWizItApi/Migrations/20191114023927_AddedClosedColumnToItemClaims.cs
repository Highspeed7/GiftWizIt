using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddedClosedColumnToItemClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "_closed",
                table: "Item_Claims",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_closed",
                table: "Item_Claims");
        }
    }
}
