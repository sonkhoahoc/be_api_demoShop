using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VHUX.Api.Migrations
{
    public partial class initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "media_file");

            migrationBuilder.AddColumn<long>(
                name: "category_id",
                table: "news",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "news",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "files",
                table: "news",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "product_file",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    file = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userAdded = table.Column<long>(type: "bigint", nullable: false),
                    userUpdated = table.Column<long>(type: "bigint", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_file", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_file");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "news");

            migrationBuilder.DropColumn(
                name: "content",
                table: "news");

            migrationBuilder.DropColumn(
                name: "files",
                table: "news");

            migrationBuilder.CreateTable(
                name: "media_file",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idtable = table.Column<long>(type: "bigint", nullable: false),
                    ipserver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    is_delete = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    name_guid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tablename = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    type = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media_file", x => x.id);
                });
        }
    }
}
