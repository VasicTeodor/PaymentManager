using System;

namespace PayPal.Service.Dtos
{
    public class PaymentRequestDto
    {
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string NameOfBook { get; set; }
        public string PaymentIntent { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }

    }
}