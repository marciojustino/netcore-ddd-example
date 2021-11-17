using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDDExample.Infra.Data.Migrations
{
    public partial class Create_User_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", nullable: false),
                    Current_Password = table.Column<string>(type: "varchar(400)", nullable: false),
                    CurrentPassword_Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Last_Password = table.Column<string>(type: "varchar(400)", nullable: true),
                    LastPassword_Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
