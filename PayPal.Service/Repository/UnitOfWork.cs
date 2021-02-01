using System;
using System.Threading.Tasks;
using PayPal.Service.Data;
using PayPal.Service.Repository.Interfaces;

namespace PayPal.Service.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PayPalContext _context;

        public UnitOfWork(PayPalContext context, IBillingPlanRequestRepository billingPlanRequestRepository, IExecutedSubscriptionRepository executedSubscriptionRepository,
            ISubscriptionRequestsRepository subscriptionRequestsRepository)
        {
            _context = context;
            BillingPlanRequests = billingPlanRequestRepository;
            ExecutedSubscriptions = executedSubscriptionRepository;
            SubscriptionRequests = subscriptionRequestsRepository;
        }

        public IBillingPlanRequestRepository BillingPlanRequests { get; set; }
        public IExecutedSubscriptionRepository ExecutedSubscriptions { get; set; }
        public ISubscriptionRequestsRepository SubscriptionRequests { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}