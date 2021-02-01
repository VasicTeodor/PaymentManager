using Issuer.Service.Data;
using Issuer.Service.Data.Entities;
using Issuer.Service.Repository.Interfaces;
using Issuer.Service.Services;
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
        private readonly ISecurityService _securityService;

        public CardRepository(IssuerDbContext context, ISecurityService securityService) : base(context)
        {
            this._context = context;
            this._securityService = securityService;
        }

        public Card GetCardByPan(string pan)
        {
            var cards = _context.Cards.Include(c => c.Account).ToList();
            foreach (var card in cards)
            {
                var restPan = _securityService.DecryptStringAes(card.Pan);
                if (restPan.Equals(pan))
                {
                    return card;
                }
            }
            return null;
            //return _context.Cards.Include(x => x.Account).FirstOrDefault(x => x.Pan.Equals(pan));
        }
    }
}
