using System;
using System.ComponentModel.DataAnnotations;

namespace PublishingCompany.Api.Data.Entities
{
    public class UserBoughtBook
    {
        public User User { get; set; }
        public Book Book { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}