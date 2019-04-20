using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddForeignKeyToUsersInGiftLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "GiftLists",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GiftLists_user_id",
                table: "GiftLists",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GiftLists_Users_user_id",
                table: "GiftLists",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftLists_Users_user_id",
                table: "GiftLists");

            migrationBuilder.DropIndex(
                name: "IX_GiftLists_user_id",
                table: "GiftLists");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "GiftLists");
        }
    }
}
