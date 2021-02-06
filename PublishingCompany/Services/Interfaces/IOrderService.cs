using System;
using System.Threading.Tasks;
using PublishingCompany.Api.Dtos;

namespace PublishingCompany.Api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> CreateOrder(OrderDto order);
        Task<bool> CompleteOrder(Guid orderId, string status);
    }
}