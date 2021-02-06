using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Dto
{
    public class PccResponseDto
    {
        public string Status { get; set; }
        public Guid AcquierOrderId { get; set; }
        public DateTime? AcquierTimestamp { get; set; }
        public Guid IssuerOrderId { get; set; }
        public DateTime? IssuerTimestamp { get; set; }
    }
}
