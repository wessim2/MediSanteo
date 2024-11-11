using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediSanteo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class permission_rolePermissions2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_permissions_name",
                table: "permissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_permissions_name",
                table: "permissions",
                column: "name");
        }
    }
}
