using System.Threading.Tasks;

namespace PayPal.Service.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IBillingPlanRequestRepository BillingPlanRequests { get; set; }
        IExecutedSubscriptionRepository ExecutedSubscriptions { get; set; }
        ISubscriptionRequestsRepository SubscriptionRequests { get; set; }
        int Complete();
        Task<int> CompleteAsync();
    }
}