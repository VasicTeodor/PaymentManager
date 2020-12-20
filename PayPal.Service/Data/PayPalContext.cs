using Microsoft.EntityFrameworkCore;
using PayPal.Service.Data.Entities;

namespace PayPal.Service.Data
{
    public class PayPalContext : DbContext
    {
        public DbSet<PaymentRequest> PaymentRequests { get; set; }

        public PayPalContext(DbContextOptions options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}