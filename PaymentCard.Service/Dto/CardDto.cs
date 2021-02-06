using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Dto
{
    public class CardDto
    {
        public string Pan { get; set; }
        public string SecurityCode { get; set; }
        public string HolderName { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
