using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenRotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "system",
                table: "UserSessions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Action",
                schema: "system",
                table: "LoginLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "system",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "Action",
                schema: "system",
                table: "LoginLogs");
        }
    }
}
