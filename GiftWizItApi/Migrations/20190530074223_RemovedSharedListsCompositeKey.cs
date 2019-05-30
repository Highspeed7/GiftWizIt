using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class RemovedSharedListsCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shared_Lists_Contacts_contact_id",
                table: "Shared_Lists");

            migrationBuilder.DropForeignKey(
                name: "FK_Shared_Lists_GiftLists_g_list_id",
                table: "Shared_Lists");

            migrationBuilder.DropForeignKey(
                name: "FK_Shared_Lists_Users_user_id",
                table: "Shared_Lists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shared_Lists",
                table: "Shared_Lists");

            migrationBuilder.RenameTable(
                name: "Shared_Lists",
                newName: "SharedLists");

            migrationBuilder.RenameIndex(
                name: "IX_Shared_Lists_g_list_id",
                table: "SharedLists",
                newName: "IX_SharedLists_g_list_id");

            migrationBuilder.RenameIndex(
                name: "IX_Shared_Lists_contact_id",
                table: "SharedLists",
                newName: "IX_SharedLists_contact_id");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "SharedLists",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ShareId",
                table: "SharedLists",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedLists",
                table: "SharedLists",
                column: "ShareId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLists_user_id",
                table: "SharedLists",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedLists_Contacts_contact_id",
                table: "SharedLists",
                column: "contact_id",
                principalTable: "Contacts",
                principalColumn: "contact_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedLists_GiftLists_g_list_id",
                table: "SharedLists",
                column: "g_list_id",
                principalTable: "GiftLists",
                principalColumn: "gift_list_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedLists_Users_user_id",
                table: "SharedLists",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedLists_Contacts_contact_id",
                table: "SharedLists");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedLists_GiftLists_g_list_id",
                table: "SharedLists");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedLists_Users_user_id",
                table: "SharedLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedLists",
                table: "SharedLists");

            migrationBuilder.DropIndex(
                name: "IX_SharedLists_user_id",
                table: "SharedLists");

            migrationBuilder.DropColumn(
                name: "ShareId",
                table: "SharedLists");

            migrationBuilder.RenameTable(
                name: "SharedLists",
                newName: "Shared_Lists");

            migrationBuilder.RenameIndex(
                name: "IX_SharedLists_g_list_id",
                table: "Shared_Lists",
                newName: "IX_Shared_Lists_g_list_id");

            migrationBuilder.RenameIndex(
                name: "IX_SharedLists_contact_id",
                table: "Shared_Lists",
                newName: "IX_Shared_Lists_contact_id");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "Shared_Lists",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shared_Lists",
                table: "Shared_Lists",
                columns: new[] { "user_id", "g_list_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Shared_Lists_Contacts_contact_id",
                table: "Shared_Lists",
                column: "contact_id",
                principalTable: "Contacts",
                principalColumn: "contact_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shared_Lists_GiftLists_g_list_id",
                table: "Shared_Lists",
                column: "g_list_id",
                principalTable: "GiftLists",
                principalColumn: "gift_list_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shared_Lists_Users_user_id",
                table: "Shared_Lists",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
