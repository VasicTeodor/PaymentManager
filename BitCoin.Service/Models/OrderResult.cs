using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoin.Service.Models
{
    public class OrderResult
    {
        public long id { get; set; }
        public string status { get; set; }
        public string price_currency { get; set; }
        public string price_amount { get; set; }
        public string receive_currency { get; set; }
        public string receive_amount { get; set; }
        public string created_at { get; set; }
        public string order_id { get; set; }
        public string payment_url { get; set; }
        public string token { get; set; }
    }
}
