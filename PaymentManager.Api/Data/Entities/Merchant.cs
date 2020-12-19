using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentManager.Api.Data.Entities
{
    public class Merchant
    {
        public Guid Id { get; set; }
        public WebStore WebStore { get; set; }
        [MaxLength(30)]
        public string MerchantPassword { get; set; }
        [MaxLength(100)]
        public string MerchantUniqueId { get; set; }
        [MaxLength(100)]
        public string MerchantUniqueStoreId { get; set; }
        public ICollection<MerchantPaymentServices> PaymentServices { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}