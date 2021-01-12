using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Data.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public Guid AcquierOrderId { get; set; }
        public DateTime? AcquierTimestamp { get; set; }
        public Guid IssuerOrderId { get; set; }
        public DateTime? IssuerTimestamp { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
    }
}
