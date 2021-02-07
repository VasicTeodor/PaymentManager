using System;
using System.Collections.Generic;

namespace PaymentManager.Api.Dtos
{
    public class CreatePaymentServiceDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsPassTrough { get; set; }
        public List<Guid> WebStores { get; set; }
    }
}