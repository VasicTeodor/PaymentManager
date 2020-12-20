using Bank.Service.Data;
using Bank.Service.Data.Entities;
using Bank.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Repositories.Implementations
{
    public class CardRepository : Repository<Card, Guid>, ICardRepository
    {
        private readonly BankDbContext _context;

        public CardRepository(BankDbContext context) : base(context)
        {
            this._context = context;
        }

        public Card GetCardByPan(string pan)
        {
            return _context.Cards.Where(x => x.Pan.Equals(pan)).FirstOrDefault();
        }
    }
}
