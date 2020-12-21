namespace Bank.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_double_todecimal_init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TableVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Pan = c.String(),
                        SecurityCode = c.Int(nullable: false),
                        HolderName = c.String(),
                        ValidTo = c.DateTime(),
                        TableVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Account_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MerchantId = c.String(),
                        MerchantPassword = c.String(),
                        TableVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Url = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(),
                        SuccessUrl = c.String(),
                        ErrorUrl = c.String(),
                        FailedUrl = c.String(),
                        Merchant_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Merchant_Id)
                .Index(t => t.Merchant_Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Guid(nullable: false),
                        Timestamp = c.DateTime(),
                        Merchant_Id = c.Guid(),
                        Payer_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Merchant_Id)
                .ForeignKey("dbo.Accounts", t => t.Payer_Id)
                .Index(t => t.Merchant_Id)
                .Index(t => t.Payer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Payer_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "Merchant_Id", "dbo.Accounts");
            DropForeignKey("dbo.Payments", "Merchant_Id", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "Id", "dbo.Clients");
            DropForeignKey("dbo.Cards", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "Payer_Id" });
            DropIndex("dbo.Transactions", new[] { "Merchant_Id" });
            DropIndex("dbo.Payments", new[] { "Merchant_Id" });
            DropIndex("dbo.Cards", new[] { "Account_Id" });
            DropIndex("dbo.Accounts", new[] { "Id" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Payments");
            DropTable("dbo.Clients");
            DropTable("dbo.Cards");
            DropTable("dbo.Accounts");
        }
    }
}
