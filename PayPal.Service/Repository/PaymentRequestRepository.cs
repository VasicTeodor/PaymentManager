using System.Threading.Tasks;
using PayPal.Service.Data;
using PayPal.Service.Data.Entities;
using PayPal.Service.Repository.Interfaces;

namespace PayPal.Service.Repository
{
    public class PaymentRequestRepository : IPaymentRequestRepository
    {
        private readonly PayPalContext _context;

        public PaymentRequestRepository(PayPalContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRequest(PaymentRequest paymentRequest)
        {
            await _context.PaymentRequests.AddAsync(paymentRequest);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}