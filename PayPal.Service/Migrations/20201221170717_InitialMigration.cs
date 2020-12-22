using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayPal.Service.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    PaymentIntent = table.Column<string>(nullable: true),
                    PaymentMethod = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SuccessUrl = table.Column<string>(nullable: true),
                    ErrorUrl = table.Column<string>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    PaymentId = table.Column<string>(nullable: true),
                    TableVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentRequests");
        }
    }
}
