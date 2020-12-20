using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayPal.Service.Data.Entities
{
    public class PaymentRequest
    {
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string NameOfBook { get; set; }
        public string PaymentIntent { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        [Timestamp]
        public byte[] TableVersion { get; set; }
    }
}