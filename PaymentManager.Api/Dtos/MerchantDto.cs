using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PaymentManager.Api.Data.Entities;

namespace PaymentManager.Api.Dtos
{
    public class MerchantDto
    {
        [MaxLength(30)]
        public string MerchantPassword { get; set; }
        [MaxLength(100)]
        public string MerchantUniqueId { get; set; }
        [MaxLength(100)]
        public string MerchantUniqueStoreId { get; set; }

        public List<Guid> PaymentOptions { get; set; } = new List<Guid>();
        public WebStore WebStore { get; set; }
    }
}