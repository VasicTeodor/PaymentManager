using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Issuer.Service.Data.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MerchantId { get; set; }
        public string MerchantPassword { get; set; }
        public virtual Account Account { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}
