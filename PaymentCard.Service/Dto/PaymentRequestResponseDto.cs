using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Dto
{
    public class PaymentRequestResponseDto
    {
        [MaxLength(256)]
        [Url]
        public string PaymentUrl { get; set; }
        public Guid PaymentId { get; set; }
    }
}
