using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto2.Migrations
{
    public partial class Nuevas_TablaP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesor_Asignatura_AsignaturaId",
                table: "Profesor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfesorAsignatura_Asignatura_AsignaturaId",
                table: "ProfesorAsignatura");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfesorAsignatura_Profesor_ProfesorId",
                table: "ProfesorAsignatura");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfesorAsignatura",
                table: "ProfesorAsignatura");

            migrationBuilder.RenameTable(
                name: "ProfesorAsignatura",
                newName: "profesorAsignatura");

            migrationBuilder.RenameIndex(
                name: "IX_ProfesorAsignatura_ProfesorId",
                table: "profesorAsignatura",
                newName: "IX_profesorAsignatura_ProfesorId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfesorAsignatura_AsignaturaId",
                table: "profesorAsignatura",
                newName: "IX_profesorAsignatura_AsignaturaId");

            migrationBuilder.RenameColumn(
                name: "AsignaturaId",
                table: "Profesor",
                newName: "Asignaturaid");

            migrationBuilder.RenameIndex(
                name: "IX_Profesor_AsignaturaId",
                table: "Profesor",
                newName: "IX_Profesor_Asignaturaid");

            migrationBuilder.AlterColumn<int>(
                name: "Asignaturaid",
                table: "Profesor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_profesorAsignatura",
                table: "profesorAsignatura",
                column: "ProfesoAsignaturaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesor_Asignatura_Asignaturaid",
                table: "Profesor",
                column: "Asignaturaid",
                principalTable: "Asignatura",
                principalColumn: "AsignaturaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_profesorAsignatura_Asignatura_AsignaturaId",
                table: "profesorAsignatura",
                column: "AsignaturaId",
                principalTable: "Asignatura",
                principalColumn: "AsignaturaId");

            migrationBuilder.AddForeignKey(
                name: "FK_profesorAsignatura_Profesor_ProfesorId",
                table: "profesorAsignatura",
                column: "ProfesorId",
                principalTable: "Profesor",
                principalColumn: "ProfesorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesor_Asignatura_Asignaturaid",
                table: "Profesor");

            migrationBuilder.DropForeignKey(
                name: "FK_profesorAsignatura_Asignatura_AsignaturaId",
                table: "profesorAsignatura");

            migrationBuilder.DropForeignKey(
                name: "FK_profesorAsignatura_Profesor_ProfesorId",
                table: "profesorAsignatura");

            migrationBuilder.DropPrimaryKey(
                name: "PK_profesorAsignatura",
                table: "profesorAsignatura");

            migrationBuilder.RenameTable(
                name: "profesorAsignatura",
                newName: "ProfesorAsignatura");

            migrationBuilder.RenameIndex(
                name: "IX_profesorAsignatura_ProfesorId",
                table: "ProfesorAsignatura",
                newName: "IX_ProfesorAsignatura_ProfesorId");

            migrationBuilder.RenameIndex(
                name: "IX_profesorAsignatura_AsignaturaId",
                table: "ProfesorAsignatura",
                newName: "IX_ProfesorAsignatura_AsignaturaId");

            migrationBuilder.RenameColumn(
                name: "Asignaturaid",
                table: "Profesor",
                newName: "AsignaturaId");

            migrationBuilder.RenameIndex(
                name: "IX_Profesor_Asignaturaid",
                table: "Profesor",
                newName: "IX_Profesor_AsignaturaId");

            migrationBuilder.AlterColumn<int>(
                name: "AsignaturaId",
                table: "Profesor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfesorAsignatura",
                table: "ProfesorAsignatura",
                column: "ProfesoAsignaturaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesor_Asignatura_AsignaturaId",
                table: "Profesor",
                column: "AsignaturaId",
                principalTable: "Asignatura",
                principalColumn: "AsignaturaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfesorAsignatura_Asignatura_AsignaturaId",
                table: "ProfesorAsignatura",
                column: "AsignaturaId",
                principalTable: "Asignatura",
                principalColumn: "AsignaturaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfesorAsignatura_Profesor_ProfesorId",
                table: "ProfesorAsignatura",
                column: "ProfesorId",
                principalTable: "Profesor",
                principalColumn: "ProfesorId");
        }
    }
}
