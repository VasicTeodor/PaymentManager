using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentManager.Api.Data.Entities
{
    public class MerchantPaymentServices
    {
        public Guid MerchantId { get; set; }
        public Merchant Merchant { get; set; }
        public Guid PaymentServiceId { get; set; }
        public PaymentService PaymentService { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}