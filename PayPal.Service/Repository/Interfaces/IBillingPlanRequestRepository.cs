using System;
using System.Threading.Tasks;
using PayPal.Service.Data.Entities;

namespace PayPal.Service.Repository.Interfaces
{
    public interface IBillingPlanRequestRepository : IRepository<BillingPlanRequest, Guid>
    {
        Task<BillingPlanRequest> GetBillingPlanById(string billingPlanId);
    }
}