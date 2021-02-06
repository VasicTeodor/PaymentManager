using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublishingCompany.Api.Data.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}