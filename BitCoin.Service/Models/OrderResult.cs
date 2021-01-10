using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoin.Service.Models
{
    public class OrderResult
    {
        public Guid Id { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string PriceCurrency { get; set; }
        public string PriceAmount { get; set; }
        public string ReceiveCurrency { get; set; }
        public string ReceiveAmount { get; set; }
        public string CreatedAt { get; set; }
        public string PaymentUrl { get; set; }
        public string Token { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}
