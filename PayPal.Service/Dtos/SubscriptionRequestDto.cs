using System;

namespace PayPal.Service.Dtos
{
    public class SubscriptionRequestDto
    {
        public string billingPlanId { get; set; }
        public Guid WebStoreId { get; set; }
    }
}