using System;

namespace PayPal.Service.Dtos
{
    public class ExecuteSubscriptionDto
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public Guid WebStoreId { get; set; }
    }
}