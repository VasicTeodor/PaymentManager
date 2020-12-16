using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentManager.Api.Data.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid MerchantOrderId { get; set; }
        public Guid AcquirerOrderId { get; set; }
        public DateTime AcquirerTimeStamp { get; set; }
        public Guid PaymentId { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}