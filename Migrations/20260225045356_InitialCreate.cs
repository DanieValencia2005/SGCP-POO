using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SGCP_POO.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    id_estudiante = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    correo_institucional = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    contraseña = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Estudian__E0B2763CD343FFC2", x => x.id_estudiante);
                });

            migrationBuilder.CreateTable(
                name: "Informacion",
                columns: table => new
                {
                    id_informacion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_estudiante = table.Column<int>(type: "integer", nullable: true),
                    correo_personal = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    edad = table.Column<int>(type: "integer", nullable: true),
                    telefono = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true),
                    habilidades = table.Column<string>(type: "text", nullable: true),
                    deficiencias = table.Column<string>(type: "text", nullable: true),
                    tiempo_dedicacion = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Informac__5E0F3B9E69DBAC86", x => x.id_informacion);
                    table.ForeignKey(
                        name: "FK__Informaci__id_es__3D5E1FD2",
                        column: x => x.id_estudiante,
                        principalTable: "Estudiante",
                        principalColumn: "id_estudiante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recurso",
                columns: table => new
                {
                    id_recurso = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_estudiante = table.Column<int>(type: "integer", nullable: false),
                    titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(700)", maxLength: 700, nullable: true),
                    palabras_clave = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    tema = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    dificultad = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    formato = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    enlace = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recurso", x => x.id_recurso);
                    table.ForeignKey(
                        name: "FK_Recurso_Estudiante_id_estudiante",
                        column: x => x.id_estudiante,
                        principalTable: "Estudiante",
                        principalColumn: "id_estudiante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TarjetaConocimiento",
                columns: table => new
                {
                    id_tarjeta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_tarjeta = table.Column<string>(type: "text", nullable: false),
                    id_estudiante = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarjetaConocimiento", x => x.id_tarjeta);
                    table.ForeignKey(
                        name: "FK_TarjetaConocimiento_Estudiante_id_estudiante",
                        column: x => x.id_estudiante,
                        principalTable: "Estudiante",
                        principalColumn: "id_estudiante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TarjetaRecurso",
                columns: table => new
                {
                    id_tarjeta_recurso = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_tarjeta = table.Column<int>(type: "integer", nullable: false),
                    id_recurso = table.Column<int>(type: "integer", nullable: false),
                    retroalimentacion = table.Column<string>(type: "text", nullable: true),
                    calificacion = table.Column<int>(type: "integer", nullable: true),
                    fecha_registro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarjetaRecurso", x => x.id_tarjeta_recurso);
                    table.ForeignKey(
                        name: "FK_TarjetaRecurso_Recurso_id_recurso",
                        column: x => x.id_recurso,
                        principalTable: "Recurso",
                        principalColumn: "id_recurso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarjetaRecurso_TarjetaConocimiento_id_tarjeta",
                        column: x => x.id_tarjeta,
                        principalTable: "TarjetaConocimiento",
                        principalColumn: "id_tarjeta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Estudian__107A1DDABF1DAFB4",
                table: "Estudiante",
                column: "correo_institucional",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Informacion_id_estudiante",
                table: "Informacion",
                column: "id_estudiante");

            migrationBuilder.CreateIndex(
                name: "IX_Recurso_id_estudiante",
                table: "Recurso",
                column: "id_estudiante");

            migrationBuilder.CreateIndex(
                name: "IX_TarjetaConocimiento_id_estudiante",
                table: "TarjetaConocimiento",
                column: "id_estudiante");

            migrationBuilder.CreateIndex(
                name: "IX_TarjetaRecurso_id_recurso",
                table: "TarjetaRecurso",
                column: "id_recurso");

            migrationBuilder.CreateIndex(
                name: "IX_TarjetaRecurso_id_tarjeta",
                table: "TarjetaRecurso",
                column: "id_tarjeta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Informacion");

            migrationBuilder.DropTable(
                name: "TarjetaRecurso");

            migrationBuilder.DropTable(
                name: "Recurso");

            migrationBuilder.DropTable(
                name: "TarjetaConocimiento");

            migrationBuilder.DropTable(
                name: "Estudiante");
        }
    }
}
