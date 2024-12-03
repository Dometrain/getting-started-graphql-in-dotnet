using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.Catalog.Migrations
{
    /// <inheritdoc />
    public partial class Messages_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSupportAgent",
                table: "Users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SessionId",
                table: "CustomerChats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChats_SessionId",
                table: "CustomerChats",
                column: "SessionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerChats_SessionId",
                table: "CustomerChats");

            migrationBuilder.DropColumn(
                name: "IsSupportAgent",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "CustomerChats");
        }
    }
}
