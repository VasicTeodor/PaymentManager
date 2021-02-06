using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PublishingCompany.Api.Data.Entities
{
    public class User 
    {
        public Guid Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserType { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsBetaReader { get; set; }
        public bool Subscribed { get; set; }
        public DateTime StartDateOfSubscription { get; set; }
        public DateTime EndDateOfSubscription { get; set; }
        public string UserRole { get; set; }
        public ICollection<UserBoughtBook> UserBoughtBooks { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }

    public enum UserRole
    {
        WRITER,
        READER
    }
}
