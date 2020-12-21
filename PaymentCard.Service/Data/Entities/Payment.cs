using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Data.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        [Url]
        public string Url { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        [Url]
        public string SuccessUrl { get; set; }
        [Url]
        public string ErrorUrl { get; set; }
        [Url]
        public string FailedUrl { get; set; }
        public virtual Account Merchant { get; set; }

        public override string ToString()
        {
            return $"Payment {Id.ToString()}, with redirection Url {Url} and Status {Status}, Amount {Amount.ToString()}. For mechant with id {Merchant.Client.MerchantId}";
        }
    }
}
