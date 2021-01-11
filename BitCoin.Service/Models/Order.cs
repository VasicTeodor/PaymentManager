using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoin.Service.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string OrderId { get; set; }
        public double PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
        public string ReceiveCurrency { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CallbackUrl { get; set; }
        public string CancelUrl { get; set; }
        public string SuccessUrl { get; set; }
        public string Token { get; set; }
        public Guid MerchantId { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}
