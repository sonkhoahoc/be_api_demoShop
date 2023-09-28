using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VHUX.Api.Migrations
{
    public partial class initial7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "payment_status_id",
                table: "order",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "order_payment",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    order_price = table.Column<double>(type: "float", nullable: false),
                    payment_order_id = table.Column<long>(type: "bigint", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    payment_status_id = table.Column<byte>(type: "tinyint", nullable: false),
                    userAdded = table.Column<long>(type: "bigint", nullable: false),
                    userUpdated = table.Column<long>(type: "bigint", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_payment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_vnpay_hitory",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_id = table.Column<long>(type: "bigint", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    client_ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<byte>(type: "tinyint", nullable: false),
                    userAdded = table.Column<long>(type: "bigint", nullable: false),
                    userUpdated = table.Column<long>(type: "bigint", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_vnpay_hitory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_vnpay_order",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    price = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    payment_method_id = table.Column<byte>(type: "tinyint", nullable: false),
                    vnp_orderinfo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    vnp_txnref = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payment_status_id = table.Column<byte>(type: "tinyint", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    status_id = table.Column<byte>(type: "tinyint", nullable: false),
                    userAdded = table.Column<long>(type: "bigint", nullable: false),
                    userUpdated = table.Column<long>(type: "bigint", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_vnpay_order", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vnpay_ipn",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vnp_tmn_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vnp_amount = table.Column<double>(type: "float", nullable: false),
                    vnp_bank_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vnp_vank_tranno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vnp_cardtype = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    vnp_paydate = table.Column<double>(type: "float", nullable: true),
                    vnp_order_info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vnp_transaction_no = table.Column<double>(type: "float", nullable: false),
                    vnp_responsecode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vnp_transaction_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vnp_txnref = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    vnp_secure_hashtype = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    vnp_securehash = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    type = table.Column<byte>(type: "tinyint", nullable: false),
                    userAdded = table.Column<long>(type: "bigint", nullable: false),
                    userUpdated = table.Column<long>(type: "bigint", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vnpay_ipn", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_payment");

            migrationBuilder.DropTable(
                name: "payment_vnpay_hitory");

            migrationBuilder.DropTable(
                name: "payment_vnpay_order");

            migrationBuilder.DropTable(
                name: "vnpay_ipn");

            migrationBuilder.DropColumn(
                name: "payment_status_id",
                table: "order");
        }
    }
}
