using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublishingCompany.Api.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal OrderPrice { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}