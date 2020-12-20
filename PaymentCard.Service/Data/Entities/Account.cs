using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Data.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}
