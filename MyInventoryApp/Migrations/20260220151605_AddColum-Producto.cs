using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyInventoryApp.Migrations
{
    /// <inheritdoc />
    public partial class AddColumProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockMin",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockMin",
                table: "Products");
        }
    }
}
