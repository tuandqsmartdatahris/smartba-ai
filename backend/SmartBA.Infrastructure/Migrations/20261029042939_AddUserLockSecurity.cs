using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLockSecurity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailedAttempts",
                schema: "system",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockUntil",
                schema: "system",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedAttempts",
                schema: "system",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockUntil",
                schema: "system",
                table: "Users");
        }
    }
}
