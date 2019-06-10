using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class ChangedFavoritesTableIdColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "id",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Favorites",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Favorites",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "id",
                table: "Favorites",
                column: "Id");
        }
    }
}
