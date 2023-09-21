using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto2.Migrations
{
    public partial class arreglop1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreP",
                table: "Profesor",
                newName: "Nombre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Profesor",
                newName: "NombreP");
        }
    }
}
