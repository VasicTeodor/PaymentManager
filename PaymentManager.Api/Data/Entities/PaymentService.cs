using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentManager.Api.Data.Entities
{
    public class PaymentService
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsPassTrough { get; set; }
        public ICollection<WebStorePaymentService> WebStores { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}