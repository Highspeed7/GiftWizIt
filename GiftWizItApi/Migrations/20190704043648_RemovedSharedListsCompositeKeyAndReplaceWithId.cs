using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class RemovedSharedListsCompositeKeyAndReplaceWithId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shared_Lists",
                table: "Shared_Lists");

            migrationBuilder.AddColumn<int>(
                name: "shared_list_id",
                table: "Shared_Lists",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shared_Lists",
                table: "Shared_Lists",
                column: "shared_list_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shared_Lists_contact_id",
                table: "Shared_Lists",
                column: "contact_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shared_Lists",
                table: "Shared_Lists");

            migrationBuilder.DropIndex(
                name: "IX_Shared_Lists_contact_id",
                table: "Shared_Lists");

            migrationBuilder.DropColumn(
                name: "shared_list_id",
                table: "Shared_Lists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shared_Lists",
                table: "Shared_Lists",
                columns: new[] { "contact_id", "g_list_id" });
        }
    }
}
