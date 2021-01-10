using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PayPal.Api;

namespace PayPal.Service.Data.Entities
{
    public class BillingPlanRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SubscriptionType { get; set; }
        public string PaymentType { get; set; }
        public string Frequency { get; set; }
        public int FrequencyInterval { get; set; }
        public int Cycles { get; set; }
        public string Currency { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        public string BillingPlanId { get; set; }
        public Guid WebStoreId { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}