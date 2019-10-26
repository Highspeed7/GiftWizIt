using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddListMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GList_Messages",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<string>(nullable: false),
                    gift_list_id = table.Column<int>(nullable: false),
                    message = table.Column<string>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GList_Messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_GList_Messages_GiftLists_gift_list_id",
                        column: x => x.gift_list_id,
                        principalTable: "GiftLists",
                        principalColumn: "gift_list_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GList_Messages_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GList_Messages_gift_list_id",
                table: "GList_Messages",
                column: "gift_list_id");

            migrationBuilder.CreateIndex(
                name: "IX_GList_Messages_user_id",
                table: "GList_Messages",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GList_Messages");
        }
    }
}
