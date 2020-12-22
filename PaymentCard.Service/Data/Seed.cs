using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Service.Data.Entities;

namespace Bank.Service.Data
{
    public class Seed
    {
        private readonly BankDbContext _context;

        public Seed(BankDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (!_context.Clients.Any())
            {
                var clientSeller = new Client() { FirstName = "ClientSeller", LastName = "ClientSeller", MerchantId = "UniqueMerchantId123", MerchantPassword = "UniqueMerchantPassword@123", Id = Guid.NewGuid() };
                var clientBuyerSuccess = new Client() { FirstName = "ClientBuyerSuccess", LastName = "ClientBuyerSuccess", Id = Guid.NewGuid() };
                var clientBuyerFailure = new Client() { Id = Guid.NewGuid(), FirstName = "ClientBuyerFailure", LastName = "ClientBuyerFailure" };
                _context.Clients.Add(clientSeller);
                _context.Clients.Add(clientBuyerSuccess);
                _context.Clients.Add(clientBuyerFailure);
                _context.SaveChanges();
                var sellerAccount = new Account() { Id = Guid.NewGuid(), Amount = 10000, Client = clientSeller };
                var buyerSuccessAccount = new Account() { Id = Guid.NewGuid(), Amount = 1000, Client = clientBuyerSuccess };
                var buyerFailureAccount = new Account() { Id = Guid.NewGuid(), Amount = 0, Client = clientBuyerFailure };
                _context.Accounts.Add(sellerAccount);
                _context.Accounts.Add(buyerSuccessAccount);
                _context.Accounts.Add(buyerFailureAccount);
                _context.SaveChanges();
                var cardClientSeller = new Card() { Id = Guid.NewGuid(), HolderName = "Visa", Pan = "123456789", SecurityCode = 1234, ValidTo = DateTime.UtcNow.Date.AddYears(3), Account = sellerAccount };
                var cardClientBuyerFailure = new Card() { Id = Guid.NewGuid(), HolderName = "Visa", Pan = "1234", SecurityCode = 2222, ValidTo = DateTime.UtcNow.Date.AddYears(3), Account = buyerFailureAccount };
                var cardClientBuyerSuccesss = new Card() { Id = Guid.NewGuid(), HolderName = "Visa", Pan = "12345678", SecurityCode = 1111, ValidTo = DateTime.UtcNow.Date.AddYears(3), Account = buyerSuccessAccount };
                _context.Cards.Add(cardClientSeller);
                _context.Cards.Add(cardClientBuyerFailure);
                _context.Cards.Add(cardClientBuyerSuccesss);
                _context.SaveChanges();
            }
        }
    }
}