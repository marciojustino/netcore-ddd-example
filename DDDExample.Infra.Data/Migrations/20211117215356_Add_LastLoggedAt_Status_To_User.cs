using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDDExample.Infra.Data.Migrations
{
    public partial class Add_LastLoggedAt_Status_To_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Last_Logged_At",
                table: "User",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 17, 18, 53, 55, 945, DateTimeKind.Local).AddTicks(7383));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Last_Logged_At",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "User");
        }
    }
}
