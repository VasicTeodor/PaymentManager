using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Data.Entities
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Pan { get; set; }
        public string SecurityCode { get; set; }
        public string HolderName { get; set; }
        public DateTime? ValidTo { get; set; }
        //ovo se ne bi trebalo mapirati kada povlacimo iz baze ja mislim
        public virtual Account Account { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}
