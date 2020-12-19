using System;
using System.Threading.Tasks;
using PaymentManager.Api.Data.Entities;

namespace PaymentManager.Api.Repository.Interfaces
{
    public interface IPaymentRequestRepository
    {
        Task<bool> SaveRequest(PaymentRequest paymentRequest);
        Task<PaymentRequest> GetRequestByMerchantOrderId(Guid merchantOrderId);
    }
}