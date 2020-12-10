using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentManager.Api.Data.Entities
{
    public class Merchant
    {
        public Guid Id { get; set; }
        public WebStore WebStore { get; set; }
        public string MerchantPassword { get; set; }
        public string MerchantUniqueId { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}