using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyInventoryApp.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnStocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OldStock",
                table: "StockMovements",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldStock",
                table: "StockMovements");
        }
    }
}
