using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PaymentManager.Api.Data.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}