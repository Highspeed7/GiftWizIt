using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class RemovedNotificationsFromContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Contacts_ContactsContactId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ContactsContactId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ContactsContactId",
                table: "Notifications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactsContactId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ContactsContactId",
                table: "Notifications",
                column: "ContactsContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Contacts_ContactsContactId",
                table: "Notifications",
                column: "ContactsContactId",
                principalTable: "Contacts",
                principalColumn: "contact_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
