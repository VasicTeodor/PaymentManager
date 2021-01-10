using System;
using PayPal.Service.Data;
using PayPal.Service.Data.Entities;
using PayPal.Service.Repository.Interfaces;

namespace PayPal.Service.Repository
{
    public class ExecutedSubscriptionRepository : Repository<ExecutedSubscription, Guid>, IExecutedSubscriptionRepository
    {
        private readonly PayPalContext _context;

        public ExecutedSubscriptionRepository(PayPalContext context) : base(context)
        {
            _context = context;
        }
    }
}