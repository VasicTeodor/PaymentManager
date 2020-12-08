using System;
using System.Threading.Tasks;
using PaymentManager.Api.Helpers;
using PaymentManager.Api.Data.Entities;

namespace PaymentManager.Api.Repository.Interfaces
{
    public interface IPaymentServiceRepository
    {
        Task<PaginationResult<PaymentService>> GetPaymentServices(int pageNumber = 1, int pageSize = 10);
        Task<PaymentService> GetPaymentServiceById(Guid id);
        Task<bool> AddPaymentService(PaymentService service);
        Task<bool> RemovePaymentService(Guid id);
        Task<bool> UpdatePaymentService(Guid id, PaymentService paymentServiceForUpdate);
    }
}