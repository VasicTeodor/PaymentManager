using System;

namespace PublishingCompany.Api.Dtos
{
    public class CompleteOrderDto
    {
        public Guid OrderId { get; set; }
        public string OrderStatus { get; set; }
    }
}