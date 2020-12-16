using System.Threading.Tasks;
using PaymentManager.Api.Models;

namespace PaymentManager.Api.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentRequest> GeneratePaymentRequest(string merchantId, decimal amount);
    }
}