using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class RemovedContactFromNotificationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "contact_id",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

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
    }
}
