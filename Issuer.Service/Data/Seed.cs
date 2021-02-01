using Issuer.Service.Data.Entities;
using Issuer.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issuer.Service.Data
{
    public class Seed
    {
        private readonly IssuerDbContext _context;
        private readonly ISecurityService _securityService;

        public Seed(IssuerDbContext context, ISecurityService securityService)
        {
            _context = context;
            _securityService = securityService;
            _context.Database.EnsureCreated();
        }
        public void SeedData()
        {
            if (!_context.Clients.Any() && !_context.Accounts.Any() && !_context.Cards.Any())
            {
                var clientOtherBankSuccess = new Client() { Id = Guid.NewGuid(), FirstName = "Pera", LastName = "Kupac" };
                var clientOtherBankFailure = new Client() { Id = Guid.NewGuid(), FirstName = "Pera", LastName = "Kupac" };
                var clientOtherBankError = new Client() { Id = Guid.NewGuid(), FirstName = "Pera", LastName = "Kupac" };
                var pcc = new Client() { Id = Guid.NewGuid(), FirstName = "Pcc", LastName = "Pcc" };
                _context.Clients.Add(clientOtherBankFailure);
                _context.Clients.Add(clientOtherBankSuccess);
                _context.Clients.Add(clientOtherBankError);
                _context.Clients.Add(pcc);
                var otherBankSuccessAccount = new Account() { Id = Guid.NewGuid(), Client = clientOtherBankSuccess, Amount = 2000 };
                var otherBankFailureAccount = new Account() { Id = Guid.NewGuid(), Client = clientOtherBankFailure, Amount = 0 };
                var otherBankErrorAccount = new Account() { Id = Guid.NewGuid(), Client = clientOtherBankError, Amount = 1999 };
                var pccAccount = new Account() { Id = Guid.NewGuid(), Client = pcc, Amount = 0 }; //pcc nema sredstava jer ne treba da ima
                _context.Accounts.Add(otherBankSuccessAccount);
                _context.Accounts.Add(otherBankFailureAccount);
                _context.Accounts.Add(otherBankErrorAccount);
                _context.Accounts.Add(pccAccount);
                //var cardOtherClientBuyerSuccess = new Card() { Id = Guid.NewGuid(), HolderName = "Pera2 Kupac", Pan = "1131225550784698", SecurityCode = "1111", ValidTo = DateTime.Now.Date.AddYears(3), Account = otherBankSuccessAccount };
                var cardOtherClientBuyerSuccess = new Card() { Id = Guid.NewGuid(), HolderName = "Pera2 Kupac", ValidTo = DateTime.Now.Date.AddYears(3), Account = otherBankSuccessAccount };
                cardOtherClientBuyerSuccess.Pan = _securityService.EncryptStringAes("1131225550784698");
                cardOtherClientBuyerSuccess.SecurityCode = _securityService.EncryptStringAes("1111");
                //var cardOtherClientBuyerFailure = new Card() { Id = Guid.NewGuid(), HolderName = "Pera2 Kupac", Pan = "1131225555784698", SecurityCode = "1111", ValidTo = DateTime.Now.Date.AddYears(3), Account = otherBankFailureAccount };
                var cardOtherClientBuyerFailure = new Card() { Id = Guid.NewGuid(), HolderName = "Pera2 Kupac", ValidTo = DateTime.Now.Date.AddYears(3), Account = otherBankFailureAccount };
                cardOtherClientBuyerFailure.Pan = _securityService.EncryptStringAes("1131225555784698");
                cardOtherClientBuyerFailure.SecurityCode = _securityService.EncryptStringAes("1111");
                //var cardOtherClientBuyerError = new Card() { Id = Guid.NewGuid(), HolderName = "Pera2 Kupac", Pan = "1130225555784698", SecurityCode = "1111", ValidTo = DateTime.Now.Date.AddYears(3), Account = otherBankErrorAccount };
                var cardOtherClientBuyerError = new Card() { Id = Guid.NewGuid(), HolderName = "Pera2 Kupac", ValidTo = DateTime.Now.Date.AddYears(3), Account = otherBankErrorAccount };
                cardOtherClientBuyerError.Pan = _securityService.EncryptStringAes("1130225555784698");
                cardOtherClientBuyerError.SecurityCode = _securityService.EncryptStringAes("1111");
                _context.Cards.Add(cardOtherClientBuyerSuccess);
                _context.Cards.Add(cardOtherClientBuyerFailure);
                _context.Cards.Add(cardOtherClientBuyerError);
                _context.SaveChanges();
            }
        }
    }
}
