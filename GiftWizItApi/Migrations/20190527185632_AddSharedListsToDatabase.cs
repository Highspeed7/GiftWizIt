using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddSharedListsToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shared_Lists",
                columns: table => new
                {
                    g_list_id = table.Column<int>(nullable: false),
                    user_id = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    email_sent = table.Column<string>(nullable: true, defaultValue: "False"),
                    contact_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shared_Lists", x => new { x.user_id, x.g_list_id });
                    table.ForeignKey(
                        name: "FK_Shared_Lists_Contacts_contact_id",
                        column: x => x.contact_id,
                        principalTable: "Contacts",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shared_Lists_GiftLists_g_list_id",
                        column: x => x.g_list_id,
                        principalTable: "GiftLists",
                        principalColumn: "gift_list_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shared_Lists_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shared_Lists_contact_id",
                table: "Shared_Lists",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shared_Lists_g_list_id",
                table: "Shared_Lists",
                column: "g_list_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shared_Lists");
        }
    }
}
