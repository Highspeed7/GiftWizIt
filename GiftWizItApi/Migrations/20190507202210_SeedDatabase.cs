using Microsoft.EntityFrameworkCore.Migrations;

namespace GiftWizItApi.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Partners (name, Domain) VALUES ('Amazon', 'https://www.amazon.com')");
            migrationBuilder.Sql("INSERT INTO Partners (name, Domain) VALUES ('Walmart', 'https://www.walmart.com')");
            migrationBuilder.Sql("INSERT INTO Partners (name, Domain) VALUES ('Target', 'https://www.target.com')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes");
        }
    }
}
