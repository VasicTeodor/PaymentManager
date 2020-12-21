using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Data.Entities
{
    public class Account
    {
        [ForeignKey("Client")]
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}
