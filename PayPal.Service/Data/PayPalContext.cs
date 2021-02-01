using Microsoft.EntityFrameworkCore;
using PayPal.Service.Data.Entities;

namespace PayPal.Service.Data
{
    public class PayPalContext : DbContext
    {
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        public DbSet<BillingPlanRequest> BillingPlanRequests { get; set; }
        public DbSet<SubscriptionRequest> SubscriptionRequests { get; set; }
        public DbSet<ExecutedSubscription> ExecutedSubscriptions { get; set; }

        public PayPalContext(DbContextOptions<PayPalContext> options): base(options)
        {
            
        }
    }
}