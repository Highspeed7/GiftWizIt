using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class ChangedEmailSentColumnToBoolForSharedListsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "email_sent",
                table: "Shared_Lists",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "False");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "email_sent",
                table: "Shared_Lists",
                nullable: true,
                defaultValue: "False",
                oldClrType: typeof(bool),
                oldDefaultValue: false);
        }
    }
}
