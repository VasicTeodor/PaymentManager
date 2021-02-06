using System;
using System.ComponentModel.DataAnnotations;

namespace PublishingCompany.Api.Data.Entities
{
    public class ShoppingCartItem
    {
        public ShoppingCart ShoppingCart { get; set; }
        public Book Book { get; set; }
        public Guid BookId { get; set; }
        public Guid ShoppingCartId { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}