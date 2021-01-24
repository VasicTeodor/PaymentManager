using System;
using PayPal.Service.Data;
using PayPal.Service.Data.Entities;
using PayPal.Service.Repository.Interfaces;

namespace PayPal.Service.Repository
{
    public class SubscriptionRequestsRepository : Repository<SubscriptionRequest, Guid>, ISubscriptionRequestsRepository
    {
        private readonly PayPalContext _context;

        public SubscriptionRequestsRepository(PayPalContext context) : base(context)
        {
            _context = context;
        }
    }
}