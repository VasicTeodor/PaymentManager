using System;
using PayPal.Service.Helpers;

namespace PayPal.Service.Dtos
{
    public class BillingPlanRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public PaymentType PaymentType { get; set; }
        public Frequency Frequency { get; set; }
        public int FrequencyInterval { get; set; }
        public int Cycles { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string SuccessUrl { get; set; }
        public string FailedUrl { get; set; }
        public Guid WebStoreId { get; set; }
    }
}