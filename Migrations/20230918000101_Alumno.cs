using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto2.Migrations
{
    public partial class Alumno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumno_Carrera_CarreraId",
                table: "Alumno");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Carrera",
                newName: "NombreC");

            migrationBuilder.AlterColumn<int>(
                name: "CarreraId",
                table: "Alumno",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreC",
                table: "Alumno",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumno_Carrera_CarreraId",
                table: "Alumno",
                column: "CarreraId",
                principalTable: "Carrera",
                principalColumn: "CarreraId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumno_Carrera_CarreraId",
                table: "Alumno");

            migrationBuilder.DropColumn(
                name: "NombreC",
                table: "Alumno");

            migrationBuilder.RenameColumn(
                name: "NombreC",
                table: "Carrera",
                newName: "Nombre");

            migrationBuilder.AlterColumn<int>(
                name: "CarreraId",
                table: "Alumno",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumno_Carrera_CarreraId",
                table: "Alumno",
                column: "CarreraId",
                principalTable: "Carrera",
                principalColumn: "CarreraId");
        }
    }
}
