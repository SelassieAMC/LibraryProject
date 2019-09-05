using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca.Migrations
{
    public partial class SeedingAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Authors (Name,LastName) VALUES ('name1','lastName1')");
            migrationBuilder.Sql("INSERT INTO Authors (Name,LastName) VALUES ('name2','lastName2')");
            migrationBuilder.Sql("INSERT INTO Authors (Name,LastName) VALUES ('name3','lastName3')");
            migrationBuilder.Sql("INSERT INTO Authors (Name,LastName) VALUES ('name4','lastName4')");
            migrationBuilder.Sql("INSERT INTO Authors (Name,LastName) VALUES ('name5','lastName5')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Authors");
        }
    }
}
