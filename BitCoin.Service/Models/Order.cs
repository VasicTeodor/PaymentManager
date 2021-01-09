using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoin.Service.Models
{
    public class Order
    {
        public string order_id { get; set; }
        public double price_amount { get; set; }
        public string price_currency { get; set; }
        public string receive_currency { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string callback_url { get; set; }
        public string cancel_url { get; set; }
        public string success_url { get; set; }
        public string token { get; set; }
    }
}
