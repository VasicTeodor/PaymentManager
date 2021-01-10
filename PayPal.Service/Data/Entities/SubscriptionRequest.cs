using System;
using System.ComponentModel.DataAnnotations;

namespace PayPal.Service.Data.Entities
{
    public class SubscriptionRequest
    {
        public Guid Id { get; set; }
        public string BillingPlanId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExecuteAgreementUrl { get; set; }
        public string ExpressCheckoutUrl { get; set; }
        public Guid WebStoreId { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}