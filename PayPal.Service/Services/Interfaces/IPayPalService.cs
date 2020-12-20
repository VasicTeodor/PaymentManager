using System.Threading.Tasks;
using PayPal.Api;
using PayPal.Service.Dtos;

namespace PayPal.Service.Services.Interfaces
{
    public interface IPayPalService
    {
        Task<Payment> CreatePayment(PaymentRequestDto paymentRequest);
        Task<Payment> ExecutePayment(string paymentId, string payerId, string email = null);
    }
}