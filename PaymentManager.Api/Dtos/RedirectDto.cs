using System;

namespace PaymentManager.Api.Dtos
{
    public class RedirectDto
    {
        public Guid OrderId { get; set; }
        public Guid MerchantId { get; set; }
        public decimal Amount { get; set; }
        public string StoreName { get; set; }
        public Guid StoreId { get; set; }
    }
}