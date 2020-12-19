using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentManager.Api.Data;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Repository.Interfaces;

namespace PaymentManager.Api.Repository
{
    public class PaymentRequestRepository : IPaymentRequestRepository
    {
        private readonly DataContext _context;

        public PaymentRequestRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRequest(PaymentRequest paymentRequest)
        {
            await _context.PaymentRequests.AddAsync(paymentRequest);

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<PaymentRequest> GetRequestByMerchantOrderId(Guid merchantOrderId)
        {
            return await _context.PaymentRequests.FirstOrDefaultAsync(pr => pr.MerchantOrderId == merchantOrderId);
        }
    }
}