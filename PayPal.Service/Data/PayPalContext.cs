using Microsoft.EntityFrameworkCore;
using PayPal.Service.Data.Entities;

namespace PayPal.Service.Data
{
    public class PayPalContext : DbContext
    {
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        public DbSet<BillingPlanRequest> BillingPlanRequests { get; set; }

        public PayPalContext(DbContextOptions<PayPalContext> options): base(options)
        {
            
        }
    }
}