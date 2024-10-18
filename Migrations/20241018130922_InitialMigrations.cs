using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuribleDotnet_back.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    idBook = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: true),
                    resume = table.Column<string>(type: "text", nullable: true),
                    coverURL = table.Column<string>(type: "text", nullable: true),
                    audioPath = table.Column<string>(type: "text", nullable: true),
                    maxPage = table.Column<int>(type: "integer", nullable: true),
                    author = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.idBook);
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    idChapter = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    chapterTitle = table.Column<string>(type: "text", nullable: true),
                    timecode = table.Column<TimeSpan[]>(type: "interval[]", nullable: true),
                    page = table.Column<int>(type: "integer", nullable: false),
                    idBook_FK = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.idChapter);
                    table.ForeignKey(
                        name: "FK_Chapters_Books_idBook_FK",
                        column: x => x.idBook_FK,
                        principalTable: "Books",
                        principalColumn: "idBook",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_idBook_FK",
                table: "Chapters",
                column: "idBook_FK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
