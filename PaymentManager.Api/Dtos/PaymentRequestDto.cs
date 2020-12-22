using System;

namespace PaymentManager.Api.Dtos
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string PaymentServiceUrl { get; set; }
        public Guid OrderId { get; set; }
    }
}