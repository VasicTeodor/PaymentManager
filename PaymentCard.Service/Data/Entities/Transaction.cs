using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Data.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public Guid OrderId { get; set; }
        public DateTime? Timestamp { get; set; }
        public virtual Account Payer { get; set; }
        public virtual Account Merchant { get; set; }

        public override string ToString()
        {
            return $"Transaction Id: {Id.ToString()}, Status: {Status}, Amount: {Amount.ToString()}, OrderId: {OrderId.ToString()}, Timestamp: {Timestamp.ToString()}";
        }
    }
}
