using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddedUserIdColumnNameToItemClaimsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Claims_Users_UserId",
                table: "Item_Claims");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Item_Claims",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Item_Claims_UserId",
                table: "Item_Claims",
                newName: "IX_Item_Claims_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Claims_Users_user_id",
                table: "Item_Claims",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Claims_Users_user_id",
                table: "Item_Claims");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Item_Claims",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_Claims_user_id",
                table: "Item_Claims",
                newName: "IX_Item_Claims_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Claims_Users_UserId",
                table: "Item_Claims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
