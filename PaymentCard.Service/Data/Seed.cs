using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Service.Data.Entities;
using Bank.Service.Services;

namespace Bank.Service.Data
{
    public class Seed
    {
        private readonly BankDbContext _context;
        private readonly ISecurityService _securityService;

        public Seed(BankDbContext context, ISecurityService securityService)
        {
            _context = context;
            _securityService = securityService;
            _context.Database.EnsureCreated();
        }

        public void SeedData()
        {
            if (!_context.Clients.Any())
            {
                var clientSeller = new Client() { FirstName = "ClientSeller", LastName = "ClientSeller", MerchantId = "UniqueMerchantId123", MerchantPassword = "UniqueMerchantPassword@123", Id = Guid.NewGuid() };
                clientSeller.MerchantId = _securityService.EncryptStringAes("UniqueMerchantId123");
                clientSeller.MerchantPassword = _securityService.EncryptStringAes("UniqueMerchantPassword@123");
                var clientBuyerSuccess = new Client() { FirstName = "ClientBuyerSuccess", LastName = "ClientBuyerSuccess", Id = Guid.NewGuid() };
                var clientBuyerFailure = new Client() { Id = Guid.NewGuid(), FirstName = "ClientBuyerFailure", LastName = "ClientBuyerFailure" };
                var pcc = new Client() { Id = Guid.NewGuid(), FirstName = "Pcc", LastName = "Pcc" };

                _context.Clients.Add(clientSeller);
                _context.Clients.Add(clientBuyerSuccess);
                _context.Clients.Add(clientBuyerFailure);

                _context.Clients.Add(pcc);
                _context.SaveChanges();
                var sellerAccount = new Account() { Id = Guid.NewGuid(), Amount = 10000, Client = clientSeller };
                var buyerSuccessAccount = new Account() { Id = Guid.NewGuid(), Amount = 1000, Client = clientBuyerSuccess };
                var buyerFailureAccount = new Account() { Id = Guid.NewGuid(), Amount = 0, Client = clientBuyerFailure };
                var pccAccount = new Account() { Id = Guid.NewGuid(), Client = pcc, Amount = 0 }; //pcc nema sredstava jer ne treba da ima

                _context.Accounts.Add(sellerAccount);
                _context.Accounts.Add(buyerSuccessAccount);
                _context.Accounts.Add(buyerFailureAccount);

                _context.Accounts.Add(pccAccount);
                _context.SaveChanges();
                var cardClientSeller = new Card() { Id = Guid.NewGuid(), HolderName = "Pera Prodavac", ValidTo = DateTime.Now.Date.AddYears(3), Account = sellerAccount };
                cardClientSeller.Pan = _securityService.EncryptStringAes("1111222233334444");
                cardClientSeller.SecurityCode = _securityService.EncryptStringAes("1234");
                var cardClientBuyerFailure = new Card() { Id = Guid.NewGuid(), HolderName = "Pera Kupac", ValidTo = DateTime.Now.Date.AddYears(3), Account = buyerFailureAccount };
                cardClientBuyerFailure.Pan = _securityService.EncryptStringAes("1111222555782698");
                cardClientBuyerFailure.SecurityCode = _securityService.EncryptStringAes("2222");
                var cardClientBuyerSuccesss = new Card() { Id = Guid.NewGuid(), HolderName = "Pera Kupac", ValidTo = DateTime.Now.Date.AddYears(3), Account = buyerSuccessAccount };
                cardClientBuyerSuccesss.Pan = _securityService.EncryptStringAes("1111222555784698");
                cardClientBuyerSuccesss.SecurityCode = _securityService.EncryptStringAes("1111");

                _context.Cards.Add(cardClientSeller);
                _context.Cards.Add(cardClientBuyerFailure);
                _context.Cards.Add(cardClientBuyerSuccesss);

                _context.SaveChanges();
            }
        }
    }
}