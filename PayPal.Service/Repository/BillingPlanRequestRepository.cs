using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<BillingPlanRequest> GetBillingPlanById(string billingPlanId)
        {
            return await _context.BillingPlanRequests.FirstOrDefaultAsync(billingPlan =>
                billingPlan.BillingPlanId.Equals(billingPlanId));
        }
    }
}