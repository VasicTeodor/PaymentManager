using System;

namespace PaymentManager.Api.Dtos
{
    public class TransactionResultDto
    {
        public Guid MerchantOrderId { get; set; }
        public Guid PaymentId { get; set; }
        public string Status { get; set; }
        public Guid AcquirerOrderId { get; set; }
        public DateTime AcquirerTimestamp { get; set; }
        public decimal Amount { get; set; }
    }
}