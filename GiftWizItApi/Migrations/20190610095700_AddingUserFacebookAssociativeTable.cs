using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddingUserFacebookAssociativeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_Facebook_Assoc",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    facebook_id = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Facebook_Assoc", x => new { x.user_id, x.facebook_id });
                    table.ForeignKey(
                        name: "FK_User_Facebook_Assoc_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Facebook_Assoc_user_id",
                table: "User_Facebook_Assoc",
                column: "user_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Facebook_Assoc");
        }
    }
}
