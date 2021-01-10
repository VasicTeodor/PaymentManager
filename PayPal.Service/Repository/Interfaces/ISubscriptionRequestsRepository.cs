using System;
using PayPal.Service.Data.Entities;

namespace PayPal.Service.Repository.Interfaces
{
    public interface ISubscriptionRequestsRepository : IRepository<SubscriptionRequest, Guid>
    {
        
    }
}