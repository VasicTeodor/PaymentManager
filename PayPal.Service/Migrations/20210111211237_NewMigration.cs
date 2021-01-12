using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayPal.Service.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingPlanRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SubscriptionType = table.Column<string>(nullable: true),
                    PaymentType = table.Column<string>(nullable: true),
                    Frequency = table.Column<string>(nullable: true),
                    FrequencyInterval = table.Column<int>(nullable: false),
                    Cycles = table.Column<int>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    BillingPlanId = table.Column<string>(nullable: true),
                    WebStoreId = table.Column<Guid>(nullable: false),
                    TableVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingPlanRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExecutedSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    WebStoreId = table.Column<Guid>(nullable: false),
                    TableVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutedSubscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BillingPlanId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ExecuteAgreementUrl = table.Column<string>(nullable: true),
                    ExpressCheckoutUrl = table.Column<string>(nullable: true),
                    WebStoreId = table.Column<Guid>(nullable: false),
                    TableVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingPlanRequests");

            migrationBuilder.DropTable(
                name: "ExecutedSubscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionRequests");
        }
    }
}
