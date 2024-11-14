using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediSanteo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class prescription_modif : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_prescription_medications",
                table: "prescription_medications");

            migrationBuilder.DropColumn(
                name: "medicament_id",
                table: "prescription_medications");

            migrationBuilder.AddPrimaryKey(
                name: "pk_prescription_medications",
                table: "prescription_medications",
                columns: new[] { "prescription_id", "medication_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_prescription_medications",
                table: "prescription_medications");

            migrationBuilder.AddColumn<Guid>(
                name: "medicament_id",
                table: "prescription_medications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_prescription_medications",
                table: "prescription_medications",
                columns: new[] { "prescription_id", "medicament_id" });
        }
    }
}
