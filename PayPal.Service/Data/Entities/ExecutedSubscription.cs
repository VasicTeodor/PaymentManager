using System;
using System.ComponentModel.DataAnnotations;

namespace PayPal.Service.Data.Entities
{
    public class ExecutedSubscription
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public Guid WebStoreId { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}