using System;

namespace PaymentManager.Api.Dtos
{
    public class SubscriptionRedirectDto
    {
        public Guid WebStoreId { get; set; }
        public string StoreName { get; set; }
        public Guid UserId { get; set; }
    }
}