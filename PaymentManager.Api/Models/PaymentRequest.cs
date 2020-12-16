using System;

namespace PaymentManager.Api.Models
{
    public class PaymentRequest
    {
        public string MerchantId { get; set; }
        public string MerchantPassword { get; set; }
        public decimal Amount { get; set; }
        public Guid MerchantOrderId { get; set; }
        public DateTime MerchantTimestamp { get; set; }
        public string SuccessUrl { get; set; }
        public string FailedUrl { get; set; }
        public string ErrorUrl { get; set; }
    }
}