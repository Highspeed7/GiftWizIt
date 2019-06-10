using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddedNotificationsFieldContactIdToNotificationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "contact_id",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "message",
                table: "Notifications",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_contact_id",
                table: "Notifications",
                column: "contact_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Contacts_contact_id",
                table: "Notifications",
                column: "contact_id",
                principalTable: "Contacts",
                principalColumn: "contact_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Contacts_contact_id",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_contact_id",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "contact_id",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "message",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "title",
                table: "Notifications");
        }
    }
}
