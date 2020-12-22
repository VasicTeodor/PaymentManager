using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Dto
{
    public class TransactionDto
    {
        public Guid MerchantOrderId { get; set; }
        public Guid PaymentId { get; set; }
        public string Status { get; set; }
        public Guid AcquirerOrderId { get; set; }
        public DateTime AcquirerTimestamp { get; set; }
        public decimal Amount { get; set; }
    }
}
