using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentManager.Api.Data.Entities
{
    public class WebStorePaymentService
    {
        public Guid WebStoreId { get; set; }
        public WebStore WebStore { get; set; }
        public Guid PaymentServiceId { get; set; }
        public PaymentService PaymentService { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}