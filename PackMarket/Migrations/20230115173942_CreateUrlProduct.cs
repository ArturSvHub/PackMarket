using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PackMarket.Migrations
{
    public partial class CreateUrlProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Products",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Products");
        }
    }
}
