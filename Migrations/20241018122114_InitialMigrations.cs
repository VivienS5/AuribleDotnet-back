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
                name: "Book",
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
                    table.PrimaryKey("PK_Book", x => x.idBook);
                });

            migrationBuilder.CreateTable(
                name: "Chapter",
                columns: table => new
                {
                    idChapter = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    chapterTitle = table.Column<string>(type: "text", nullable: true),
                    timecode = table.Column<TimeSpan[]>(type: "interval[]", nullable: true),
                    page = table.Column<int>(type: "integer", nullable: false),
                    BookidBook = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapter", x => x.idChapter);
                    table.ForeignKey(
                        name: "FK_Chapter_Book_BookidBook",
                        column: x => x.BookidBook,
                        principalTable: "Book",
                        principalColumn: "idBook",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapter_BookidBook",
                table: "Chapter",
                column: "BookidBook");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapter");

            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
