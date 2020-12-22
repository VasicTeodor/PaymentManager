using System;

namespace PayPal.Service.Dtos
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentIntent { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public string SuccessUrl { get; set; }
        public string ErrorUrl { get; set; }
        public Guid OrderId { get; set; }
    }
}