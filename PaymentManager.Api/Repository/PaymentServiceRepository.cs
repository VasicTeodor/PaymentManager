using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using PaymentManager.Api.Data;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Helpers;
using PaymentManager.Api.Repository.Interfaces;

namespace PaymentManager.Api.Repository
{
    public class PaymentServiceRepository : IPaymentServiceRepository
    {
        private readonly DataContext _context;

        public PaymentServiceRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<bool> AddPaymentService(PaymentService service)
        {
            await _context.AddAsync(service);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PaymentService> GetPaymentServiceById(Guid id)
        {
            return await _context.PaymentServices.Include(ps => ps.WebStores).ThenInclude(wps => wps.WebStore).FirstOrDefaultAsync(ps => ps.Id == id);
        }

        public async Task<PaginationResult<PaymentService>> GetPaymentServices(int pageNumber = 1, int pageSize = 10)
        {
            var result = new PaginationResult<PaymentService>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                NumberOfItems = await _context.PaymentServices.CountAsync(),
                Items = await _context.PaymentServices.Skip(pageSize * (pageNumber - 1)).Take(pageSize)
                    .ToListAsync()
            };

            return result;
        }

        public async Task<bool> RemovePaymentService(Guid id)
        {
            var serviceToRemove = await _context.PaymentServices.FirstOrDefaultAsync(ps => ps.Id == id);
            _context.PaymentServices.Remove(serviceToRemove);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePaymentService(Guid id, PaymentService paymentServiceForUpdate)
        {
            _context.PaymentServices.Update(paymentServiceForUpdate);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
