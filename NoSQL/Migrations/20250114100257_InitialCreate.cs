using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isbn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tytul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    autor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataWydanie = table.Column<DateTime>(type: "datetime2", nullable: true),
                    gatunek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cena = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
