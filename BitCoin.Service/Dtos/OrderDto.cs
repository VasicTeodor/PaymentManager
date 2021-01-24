using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoin.Service.Dtos
{
    public class OrderDto
    {
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

        //public string order_id { get; set; }
        //public double price_amount { get; set; }
        //public string price_currency { get; set; }
        //public string receive_currency { get; set; }
        //public string title { get; set; }
        //public string description { get; set; }
        //public string callback_url { get; set; }
        //public string cancel_url { get; set; }
        //public string success_url { get; set; }
        //public string token { get; set; }
    }
}
