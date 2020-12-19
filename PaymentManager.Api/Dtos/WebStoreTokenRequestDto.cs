using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentManager.Api.Dtos
{
    public class WebStoreTokenRequestDto
    {
        [Required]
        public Guid WebStoreId { get; set; }
        [Required]
        [StringLength(20)]
        public string WebStoreName { get; set; }
    }
}