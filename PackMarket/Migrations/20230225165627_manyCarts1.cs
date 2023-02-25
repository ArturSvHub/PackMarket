using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PackMarket.Migrations
{
    public partial class manyCarts1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProduct");

            migrationBuilder.AddColumn<string>(
                name: "CartProducts",
                table: "Carts",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartProducts",
                table: "Carts");

            migrationBuilder.CreateTable(
                name: "CartProduct",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "integer", nullable: false),
                    cartsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => new { x.ProductsId, x.cartsId });
                    table.ForeignKey(
                        name: "FK_CartProduct_Carts_cartsId",
                        column: x => x.cartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_cartsId",
                table: "CartProduct",
                column: "cartsId");
        }
    }
}
