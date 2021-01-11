using Bank.Service.Data;
using Bank.Service.Data.Entities;
using Bank.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bank.Service.Services;

namespace Bank.Service.Repositories.Implementations
{
    public class CardRepository : Repository<Card, Guid>, ICardRepository
    {
        private readonly BankDbContext _context;
        private readonly ISecurityService _securityService;

        public CardRepository(BankDbContext context, ISecurityService securityService) : base(context)
        {
            this._context = context;
            this._securityService = securityService;
        }

        public Card GetCardByPan(string pan)
        {
            string encryptedPan = _securityService.EncryptStringAes(pan);
            return _context.Cards.Include(c => c.Account).FirstOrDefault(x => x.Pan.Equals(encryptedPan));
        }
    }
}
