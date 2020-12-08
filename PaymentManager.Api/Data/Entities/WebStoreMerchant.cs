using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentManager.Api.Data.Entities
{
    public class WebStoreMerchant
    {
        public Guid WebStoreId { get; set; }
        public WebStore WebStore { get; set; }
        public Guid MerchantId { get; set; }
        public Merchant Merchant { get; set; }
        [Timestamp] 
        public byte[] TableVersion { get; set; }
    }
}