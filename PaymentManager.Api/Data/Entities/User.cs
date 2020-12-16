using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PaymentManager.Api.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string UserType { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}