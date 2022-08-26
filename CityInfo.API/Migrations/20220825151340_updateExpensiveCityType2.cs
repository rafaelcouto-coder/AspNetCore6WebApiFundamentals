using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.API.Migrations
{
    public partial class updateExpensiveCityType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpensiveCityType",
                table: "Cities",
                newName: "ExpensiveCity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpensiveCity",
                table: "Cities",
                newName: "ExpensiveCityType");
        }
    }
}
