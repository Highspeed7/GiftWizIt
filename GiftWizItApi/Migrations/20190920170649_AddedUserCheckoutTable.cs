using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddedUserCheckoutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCheckout",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    checkout_id = table.Column<string>(nullable: false),
                    completed = table.Column<bool>(nullable: false, defaultValue: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    date_completed = table.Column<DateTime>(nullable: false),
                    web_url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCheckout", x => new { x.user_id, x.checkout_id });
                    table.ForeignKey(
                        name: "FK_UserCheckout_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCheckout");
        }
    }
}
