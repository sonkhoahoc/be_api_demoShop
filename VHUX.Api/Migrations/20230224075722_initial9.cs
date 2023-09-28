using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VHUX.Api.Migrations
{
    public partial class initial9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "link_affliate",
                table: "customer");

            migrationBuilder.AddColumn<string>(
                name: "affliate",
                table: "customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "birthday",
                table: "customer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "customer_affliate",
                table: "customer",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "point",
                table: "customer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "affliate",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "birthday",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "customer_affliate",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "point",
                table: "customer");

            migrationBuilder.AddColumn<string>(
                name: "link_affliate",
                table: "customer",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
