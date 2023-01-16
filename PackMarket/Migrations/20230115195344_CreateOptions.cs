using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PackMarket.Migrations
{
    public partial class CreateOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagesPath",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagesPath",
                table: "Categories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OptionListId",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionList_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OptionOptionList",
                columns: table => new
                {
                    OptionListsId = table.Column<int>(type: "integer", nullable: false),
                    OptionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionOptionList", x => new { x.OptionListsId, x.OptionsId });
                    table.ForeignKey(
                        name: "FK_OptionOptionList_Option_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "Option",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OptionOptionList_OptionList_OptionListsId",
                        column: x => x.OptionListsId,
                        principalTable: "OptionList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_OptionListId",
                table: "Categories",
                column: "OptionListId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionList_CategoryId",
                table: "OptionList",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionOptionList_OptionsId",
                table: "OptionOptionList",
                column: "OptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_OptionList_OptionListId",
                table: "Categories",
                column: "OptionListId",
                principalTable: "OptionList",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_OptionList_OptionListId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "OptionOptionList");

            migrationBuilder.DropTable(
                name: "Option");

            migrationBuilder.DropTable(
                name: "OptionList");

            migrationBuilder.DropIndex(
                name: "IX_Categories_OptionListId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImagesPath",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImagesPath",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "OptionListId",
                table: "Categories");
        }
    }
}
