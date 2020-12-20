using System.Threading.Tasks;
using PayPal.Service.Data.Entities;

namespace PayPal.Service.Repository.Interfaces
{
    public interface IPaymentRequestRepository
    {
        Task<bool> SaveRequest(PaymentRequest paymentRequest);
    }
}