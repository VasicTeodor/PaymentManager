using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PublishingCompany.Api.Data.Entities;

namespace PublishingCompany.Api.Repository.Interfaces
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<bool> AddNewOrder(Order newOrder);
        Task<Order> GetOrderWithItems(Guid orderId);
    }
}