using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentManager.Api.Dtos
{
    public class PaymentRequestResponseDto
    {
        [MaxLength(256)]
        [Url]
        public string PaymentUrl { get; set; }
        public Guid PaymentId { get; set; }
    }
}