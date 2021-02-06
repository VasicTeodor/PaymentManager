using System;
using System.ComponentModel.DataAnnotations;

namespace PublishingCompany.Api.Data.Entities
{
    public class OrderItem
    {
        public Book Book { get; set; }
        public Guid BookId { get; set; }
        public Order Order { get; set; }
        public Guid OrderId { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}