using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto2.Migrations
{
    public partial class Nuevas_Tablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AsignaturaId",
                table: "Profesor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Asignatura",
                columns: table => new
                {
                    AsignaturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CarreaId = table.Column<int>(type: "int", nullable: false),
                    CarreraId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignatura", x => x.AsignaturaId);
                    table.ForeignKey(
                        name: "FK_Asignatura_Carrera_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carrera",
                        principalColumn: "CarreraId");
                });

            migrationBuilder.CreateTable(
                name: "ProfesorAsignatura",
                columns: table => new
                {
                    ProfesoAsignaturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorId = table.Column<int>(type: "int", nullable: true),
                    AsignaturaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesorAsignatura", x => x.ProfesoAsignaturaId);
                    table.ForeignKey(
                        name: "FK_ProfesorAsignatura_Asignatura_AsignaturaId",
                        column: x => x.AsignaturaId,
                        principalTable: "Asignatura",
                        principalColumn: "AsignaturaId");
                    table.ForeignKey(
                        name: "FK_ProfesorAsignatura_Profesor_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesor",
                        principalColumn: "ProfesorId");
                });

            migrationBuilder.CreateTable(
                name: "Tarea",
                columns: table => new
                {
                    TareaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eliminar = table.Column<bool>(type: "bit", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    AsignaturaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarea", x => x.TareaId);
                    table.ForeignKey(
                        name: "FK_Tarea_Asignatura_AsignaturaId",
                        column: x => x.AsignaturaId,
                        principalTable: "Asignatura",
                        principalColumn: "AsignaturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarea_Profesor_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesor",
                        principalColumn: "ProfesorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profesor_AsignaturaId",
                table: "Profesor",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Asignatura_CarreraId",
                table: "Asignatura",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesorAsignatura_AsignaturaId",
                table: "ProfesorAsignatura",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesorAsignatura_ProfesorId",
                table: "ProfesorAsignatura",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_AsignaturaId",
                table: "Tarea",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_ProfesorId",
                table: "Tarea",
                column: "ProfesorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesor_Asignatura_AsignaturaId",
                table: "Profesor",
                column: "AsignaturaId",
                principalTable: "Asignatura",
                principalColumn: "AsignaturaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesor_Asignatura_AsignaturaId",
                table: "Profesor");

            migrationBuilder.DropTable(
                name: "ProfesorAsignatura");

            migrationBuilder.DropTable(
                name: "Tarea");

            migrationBuilder.DropTable(
                name: "Asignatura");

            migrationBuilder.DropIndex(
                name: "IX_Profesor_AsignaturaId",
                table: "Profesor");

            migrationBuilder.DropColumn(
                name: "AsignaturaId",
                table: "Profesor");
        }
    }
}
