using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentManager.Api.Data.Entities
{
    public class PaymentRequest
    {
        public Guid Id { get; set; }
        public Guid MerchantId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        public DateTime MerchantTimestamp { get; set; }
        public Guid MerchantOrderId { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}