using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Dto
{
    public class RequestDto
    {
        public CardDto CardData { get; set; }
        public decimal Amount { get; set; }
        public Guid OrderId { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
