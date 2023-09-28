using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VHUX.Api.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "customer_address",
                table: "order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "customer_dictrics_id",
                table: "order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "customer_id",
                table: "order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "customer_name",
                table: "order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "customer_note",
                table: "order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "customer_phone",
                table: "order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "customer_provice_id",
                table: "order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "customer_wards_id",
                table: "order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "shiping_cost",
                table: "order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "total_price",
                table: "order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "total_product",
                table: "order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "order_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    userAdded = table.Column<long>(type: "bigint", nullable: false),
                    userUpdated = table.Column<long>(type: "bigint", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_detail", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_detail");

            migrationBuilder.DropColumn(
                name: "customer_address",
                table: "order");

            migrationBuilder.DropColumn(
                name: "customer_dictrics_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "customer_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "customer_name",
                table: "order");

            migrationBuilder.DropColumn(
                name: "customer_note",
                table: "order");

            migrationBuilder.DropColumn(
                name: "customer_phone",
                table: "order");

            migrationBuilder.DropColumn(
                name: "customer_provice_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "customer_wards_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "shiping_cost",
                table: "order");

            migrationBuilder.DropColumn(
                name: "total_price",
                table: "order");

            migrationBuilder.DropColumn(
                name: "total_product",
                table: "order");
        }
    }
}
