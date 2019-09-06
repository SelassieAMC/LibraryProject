using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca.Migrations
{
    public partial class SeedingCategoriesBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Categories",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Books",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.Sql("INSERT INTO Categories (Name,Description) VALUES ('category1','this is a category 1')");
            migrationBuilder.Sql("INSERT INTO Categories (Name,Description) VALUES ('category2','this is a category 2')");
            migrationBuilder.Sql("INSERT INTO Categories (Name,Description) VALUES ('category3','this is a category 3')");
            migrationBuilder.Sql("INSERT INTO Categories (Name,Description) VALUES ('category4','this is a category 4')");
            migrationBuilder.Sql("INSERT INTO Categories (Name,Description) VALUES ('category5','this is a category 5')");
            migrationBuilder.Sql("INSERT INTO Categories (Name,Description) VALUES ('category6','this is a category 6')");

            migrationBuilder.Sql("INSERT INTO Books (Name,AuthorId,ISBN) VALUES ('Book1',(SELECT Id FROM Authors WHERE Name = 'name3'),'asda-asd-asd-asd-asd1')");
            migrationBuilder.Sql("INSERT INTO Books (Name,AuthorId,ISBN) VALUES ('Book2',(SELECT Id FROM Authors WHERE Name = 'name4'),'asda-asd-asd-asd-asd2')");
            migrationBuilder.Sql("INSERT INTO Books (Name,AuthorId,ISBN) VALUES ('Book3',(SELECT Id FROM Authors WHERE Name = 'name5'),'asda-asd-asd-asd-asd3')");
            migrationBuilder.Sql("INSERT INTO Books (Name,AuthorId,ISBN) VALUES ('Book4',(SELECT Id FROM Authors WHERE Name = 'name3'),'asda-asd-asd-asd-asd4')");
            migrationBuilder.Sql("INSERT INTO Books (Name,AuthorId,ISBN) VALUES ('Book5',(SELECT Id FROM Authors WHERE Name = 'name4'),'asda-asd-asd-asd-asd5')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Books",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.Sql("TRUNCATE TABLE Categories");
            migrationBuilder.Sql("TRUNCATE TABLE Books");
        }
    }
}
