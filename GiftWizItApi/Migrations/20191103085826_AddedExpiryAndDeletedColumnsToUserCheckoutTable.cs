using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class AddedExpiryAndDeletedColumnsToUserCheckoutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "UserCheckout",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "expiry_date",
                table: "UserCheckout",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted",
                table: "UserCheckout");

            migrationBuilder.DropColumn(
                name: "expiry_date",
                table: "UserCheckout");
        }
    }
}
