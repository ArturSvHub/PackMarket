using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PackMarket.Migrations
{
    public partial class AddCartAndPromo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_OptionList_OptionListId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionList_Categories_CategoryId",
                table: "OptionList");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionOptionList_Option_OptionsId",
                table: "OptionOptionList");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionOptionList_OptionList_OptionListsId",
                table: "OptionOptionList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionList",
                table: "OptionList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Option",
                table: "Option");

            migrationBuilder.RenameTable(
                name: "OptionList",
                newName: "OptionLists");

            migrationBuilder.RenameTable(
                name: "Option",
                newName: "Options");

            migrationBuilder.RenameIndex(
                name: "IX_OptionList_CategoryId",
                table: "OptionLists",
                newName: "IX_OptionLists_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromoId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionLists",
                table: "OptionLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Options",
                table: "Options",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Percentage = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CartId",
                table: "Products",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CartId",
                table: "AspNetUsers",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PromoId",
                table: "AspNetUsers",
                column: "PromoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Promos_UserId",
                table: "Promos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Carts_CartId",
                table: "AspNetUsers",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Promos_PromoId",
                table: "AspNetUsers",
                column: "PromoId",
                principalTable: "Promos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_OptionLists_OptionListId",
                table: "Categories",
                column: "OptionListId",
                principalTable: "OptionLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionLists_Categories_CategoryId",
                table: "OptionLists",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionOptionList_OptionLists_OptionListsId",
                table: "OptionOptionList",
                column: "OptionListsId",
                principalTable: "OptionLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionOptionList_Options_OptionsId",
                table: "OptionOptionList",
                column: "OptionsId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Carts_CartId",
                table: "Products",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Carts_CartId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Promos_PromoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_OptionLists_OptionListId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionLists_Categories_CategoryId",
                table: "OptionLists");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionOptionList_OptionLists_OptionListsId",
                table: "OptionOptionList");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionOptionList_Options_OptionsId",
                table: "OptionOptionList");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Carts_CartId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Promos");

            migrationBuilder.DropIndex(
                name: "IX_Products_CartId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CartId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PromoId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Options",
                table: "Options");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionLists",
                table: "OptionLists");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PromoId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Options",
                newName: "Option");

            migrationBuilder.RenameTable(
                name: "OptionLists",
                newName: "OptionList");

            migrationBuilder.RenameIndex(
                name: "IX_OptionLists_CategoryId",
                table: "OptionList",
                newName: "IX_OptionList_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Option",
                table: "Option",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionList",
                table: "OptionList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_OptionList_OptionListId",
                table: "Categories",
                column: "OptionListId",
                principalTable: "OptionList",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionList_Categories_CategoryId",
                table: "OptionList",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionOptionList_Option_OptionsId",
                table: "OptionOptionList",
                column: "OptionsId",
                principalTable: "Option",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionOptionList_OptionList_OptionListsId",
                table: "OptionOptionList",
                column: "OptionListsId",
                principalTable: "OptionList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
