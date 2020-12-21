using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayPal.Service.Data.Entities
{
    public class PaymentRequest
    {
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentIntent { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public string SuccessUrl { get; set; }
        public string ErrorUrl { get; set; }
        public Guid OrderId { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}