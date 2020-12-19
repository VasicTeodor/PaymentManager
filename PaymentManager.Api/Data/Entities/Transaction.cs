using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }

        public override string ToString()
        {
            return $"Transaction id: {Id}, with merchant order id: {MerchantOrderId}, acquirer order id: {AcquirerOrderId}, on date {AcquirerTimeStamp:U}, for amount: {Amount} finished with status: {Status}";
        }
    }
}