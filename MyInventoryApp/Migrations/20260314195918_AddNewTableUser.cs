using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyInventoryApp.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationToken_UserId",
                table: "NotificationToken",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationToken_User_UserId",
                table: "NotificationToken",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationToken_User_UserId",
                table: "NotificationToken");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_NotificationToken_UserId",
                table: "NotificationToken");
        }
    }
}
