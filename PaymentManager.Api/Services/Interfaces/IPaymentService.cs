using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Dtos;
using PaymentManager.Api.Models;
using PaymentRequest = PaymentManager.Api.Models.PaymentRequest;

namespace PaymentManager.Api.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentRequest> GeneratePaymentRequest(string merchantId, decimal amount);
        Task<CompleteTransactionResult> CompleteTransaction(Transaction transaction);
        Task<List<PaymentServiceDto>> GetPaymentOptions(string merchantStoreId);
    }
}