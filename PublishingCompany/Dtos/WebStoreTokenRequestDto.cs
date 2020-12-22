using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PublishingCompany.Api.Dtos
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
