using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentManager.Api.Data.Entities
{
    public class WebStore
    {
        public Guid Id { get; set; }
        public string SuccessUrl { get; set; }
        public string ErrorUrl { get; set; }
        public string FailureUrl { get; set; }
        public string Url { get; set; }
        public bool SingleMerchantStore { get; set; }
        public ICollection<WebStorePaymentService> PaymentOptions { get; set; }
        public ICollection<Merchant> Merchants { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}