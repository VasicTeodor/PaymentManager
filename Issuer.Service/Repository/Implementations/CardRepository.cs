using Issuer.Service.Data;
using Issuer.Service.Data.Entities;
using Issuer.Service.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issuer.Service.Repository.Implementations
{
    public class CardRepository : Repository<Card, Guid>, ICardRepository
    {
        private readonly IssuerDbContext _context;

        public CardRepository(IssuerDbContext context) : base(context)
        {
            this._context = context;
        }

        public Card GetCardByPan(string pan)
        {
            //var cards = _context.Cards.Include(c => c.Account).ToList();
            //Card c = new Card();
            //foreach (var card in cards)
            //{
            //    card.Pan = _securityService.DecryptStringAes(card.Pan);
            //    if (card.Pan.Equals(pan))
            //    {
            //        c = card;
            //        return c;
            //    }
            //}
            //return c;
            return _context.Cards.Include(x => x.Account).FirstOrDefault(x => x.Pan.Equals(pan));
        }
    }
}
