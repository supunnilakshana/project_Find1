using Microsoft.EntityFrameworkCore.Migrations;

namespace Find1.Data.Migrations
{
    public partial class m9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Disription",
                table: "Ads",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disription",
                table: "Ads");
        }
    }
}
