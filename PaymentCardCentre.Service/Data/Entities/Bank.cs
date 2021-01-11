using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Data.Entities
{
    public class Bank
    {
        public Guid Id { get; set; }
        public string Pan { get; set; }
    }
}
