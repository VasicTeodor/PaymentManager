using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayPal.Api;
using PayPal.Service.Data.Entities;
using PayPal.Service.Dtos;

namespace PayPal.Service.Services.Interfaces
{
    public interface ISubscriptionService
    {
        Task<Plan> CreateBillingPlan(BillingPlanRequestDto billingPlan);
        Task<Agreement> CreateSubscription(SubscriptionRequestDto subscriptionRequest);
        Task<Agreement> ExecuteSubscription(ExecuteSubscriptionDto executeSubscription);
        Task<IEnumerable<SubscriptionRequest>> GetSubscriptions(GetSubscriptionsDto getSubscriptions);
    }
}
