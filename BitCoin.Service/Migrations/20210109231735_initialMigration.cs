using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BitCoin.Service.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    PriceAmount = table.Column<double>(nullable: false),
                    PriceCurrency = table.Column<string>(nullable: true),
                    ReceiveCurrency = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CallbackUrl = table.Column<string>(nullable: true),
                    CancelUrl = table.Column<string>(nullable: true),
                    SuccessUrl = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    TableVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    PriceCurrency = table.Column<string>(nullable: true),
                    PriceAmount = table.Column<string>(nullable: true),
                    ReceiveCurrency = table.Column<string>(nullable: true),
                    ReceiveAmount = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<string>(nullable: true),
                    PaymentUrl = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    TableVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderResult", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "OrderResult");
        }
    }
}
