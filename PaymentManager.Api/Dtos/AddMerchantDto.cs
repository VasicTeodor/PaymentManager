using System;
using System.Collections.Generic;

namespace PaymentManager.Api.Dtos
{
    public class AddMerchantDto
    {
        public string MerchantUniqueId { get; set; }
        public string MerchantPassword { get; set; }
        public string MerchantUniqueStoreId { get; set; }
        public Guid WebStoreId { get; set; }
        public List<Guid> PaymentServices { get; set; }
    }
}