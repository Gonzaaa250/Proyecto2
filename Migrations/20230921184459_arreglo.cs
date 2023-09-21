using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto2.Migrations
{
    public partial class arreglo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Alumno");

            migrationBuilder.DropColumn(
                name: "NombreA",
                table: "Alumno");

            migrationBuilder.RenameColumn(
                name: "NombreC",
                table: "Alumno",
                newName: "Nombre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Alumno",
                newName: "NombreC");

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Alumno",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreA",
                table: "Alumno",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
