using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentManager.Api.Models
{
    public class PaymentRequest
    {
        [MaxLength(30)]
        public string MerchantId { get; set; }
        [MaxLength(100)]
        public string MerchantPassword { get; set; }
        public decimal Amount { get; set; }
        public Guid MerchantOrderId { get; set; }
        public DateTime MerchantTimestamp { get; set; }
        public string SuccessUrl { get; set; }
        public string FailedUrl { get; set; }
        public string ErrorUrl { get; set; }

        public override string ToString()
        {
            return
                $"Payment request with merchant id {MerchantId}, merchant order id {MerchantOrderId}, on date {MerchantTimestamp:U} and amount {Amount}";
        }
    }
}