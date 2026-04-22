using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyInventoryApp.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolType",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RolType",
                table: "Users");
        }
    }
}
