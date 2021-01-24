using System;

namespace BitCoin.Service.Dtos
{
    public class TransactionDto
    {
        public Guid MerchantOrderId { get; set; }
        public Guid PaymentId { get; set; }
        public string Status { get; set; }
        public Guid AcquirerOrderId { get; set; }
        public DateTime AcquirerTimestamp { get; set; }
        public decimal Amount { get; set; }
    }
}