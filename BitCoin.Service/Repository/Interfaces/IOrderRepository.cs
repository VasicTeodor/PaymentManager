using BitCoin.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoin.Service.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> SaveOrder(Order order);
        Task<Order> GetOrder(string orderId);
        Task<bool> SaveOrderResult(OrderResult orderResult);
        Task<OrderResult> GetOrderResult(string orderResultId);
    }
}
