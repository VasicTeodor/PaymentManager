using System;
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
        Task<PaymentRequest> GeneratePaymentRequest(decimal amount, Guid orderId);
        Task<CompleteTransactionResult> CompleteTransaction(Transaction transaction);
        Task<List<PaymentServiceDto>> GetPaymentOptions(Guid merchantId);
        Task<List<PaymentServiceDto>> GetPaymentOptionsForOrder(Guid orderId);
        Task<bool> SavePaymentRequest(RedirectDto redirectDto);
        Task<Data.Entities.PaymentRequest> GetPaymentRequestDetails(Guid orderId);
    }
}