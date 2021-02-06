using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublishingCompany.Api.Data.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Writers { get; set; }
        public string Genre { get; set; }
        public long Isbn { get; set; }
        public string Publisher { get; set; }
        public string PlaceOfPublishing { get; set; }
        public int NumberOfPages { get; set; }
        public string Synopsis { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public ICollection<UserBoughtBook> UserBoughtBooks { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}