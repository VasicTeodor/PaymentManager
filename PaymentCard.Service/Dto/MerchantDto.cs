using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Dto
{
    public class MerchantDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MerchantId { get; set; }
        public string MerchantPassword { get; set; }
        public decimal Amount { get; set; }
        public string Pan { get; set; }
        public string SecurityCode { get; set; }
        public string HolderName { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
