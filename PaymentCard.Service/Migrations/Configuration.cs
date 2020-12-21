namespace Bank.Service.Migrations
{
    using Bank.Service.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bank.Service.Data.BankDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Bank.Service.Data.BankDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var clientSeller = new Client() { FirstName = "ClientSeller", LastName = "ClientSeller", MerchantId = "UniqueMerchantId123", MerchantPassword = "UniqueMerchantPassword@123", Id= Guid.NewGuid() };
            var clientBuyerSuccess = new Client() { FirstName = "ClientBuyerSuccess", LastName = "ClientBuyerSuccess", Id = Guid.NewGuid() };
            var clientBuyerFailure = new Client() { Id = Guid.NewGuid(), FirstName = "ClientBuyerFailure", LastName = "ClientBuyerFailure" };
            context.Clients.Add(clientSeller);
            context.Clients.Add(clientBuyerSuccess);
            context.Clients.Add(clientBuyerFailure);
            //context.SaveChanges();
            var sellerAccount = new Account() { Id = Guid.NewGuid(), Amount = 10000, Client = clientSeller };
            var buyerSuccessAccount = new Account() { Id = Guid.NewGuid(), Amount = 1000, Client = clientBuyerSuccess };
            var buyerFailureAccount = new Account() { Id = Guid.NewGuid(), Amount = 0, Client = clientBuyerFailure };
            context.Accounts.Add(sellerAccount);
            context.Accounts.Add(buyerSuccessAccount);
            context.Accounts.Add(buyerFailureAccount);
            //context.SaveChanges();
            var cardClientSeller = new Card() { Id = Guid.NewGuid(), HolderName = "Visa", Pan = "12345678", SecurityCode = 1234, ValidTo = DateTime.UtcNow.Date.AddYears(3), Account = sellerAccount };
            sellerAccount.Cards = new List<Card>() { cardClientSeller };
            var cardClientBuyerSuccesss = new Card() { Id = Guid.NewGuid(), HolderName = "Visa", Pan = "12345678", SecurityCode = 1111, ValidTo = DateTime.UtcNow.Date.AddYears(3), Account = buyerSuccessAccount };
            buyerSuccessAccount.Cards = new List<Card>() { cardClientBuyerSuccesss };
            var cardClientBuyerFailure = new Card() { Id = Guid.NewGuid(), HolderName = "Visa", Pan = "1234", SecurityCode = 2222, ValidTo = DateTime.UtcNow.Date.AddYears(3), Account = buyerFailureAccount };
            buyerFailureAccount.Cards = new List<Card>() { cardClientBuyerFailure };
            context.SaveChanges();

        }
    }
}
