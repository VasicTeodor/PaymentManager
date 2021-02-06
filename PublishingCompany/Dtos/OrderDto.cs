using System;
using System.Collections.Generic;

namespace PublishingCompany.Api.Dtos
{
    public class OrderDto
    {
        public decimal OrderPrice { get; set; }
        public List<Guid> BookIds { get; set; }
        public Guid UserId { get; set; }
    }
}