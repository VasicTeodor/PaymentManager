using System;
using PayPal.Service.Data;
using PayPal.Service.Data.Entities;
using PayPal.Service.Repository.Interfaces;

namespace PayPal.Service.Repository
{
    public class BillingPlanRequestRepository : Repository<BillingPlanRequest,Guid>,IBillingPlanRequestRepository
    {
        private readonly PayPalContext _context;

        public BillingPlanRequestRepository(PayPalContext context) : base(context)
        {
            this._context = context;
        }
    }
}