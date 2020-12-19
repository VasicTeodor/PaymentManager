using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Data;
using PaymentManager.Api.Helpers;
using PaymentManager.Api.Repository.Interfaces;

namespace PaymentManager.Api.Repository
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly DataContext _context;

        public MerchantRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<PaginationResult<Merchant>> GetMerchants(int pageNumber = 1, int pageSize = 10)
        {
            var result = new PaginationResult<Merchant>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                NumberOfItems = await _context.Merchants.CountAsync(),
                Items = await _context.Merchants
                    .Include(m => m.WebStore)
                    .Include(m => m.PaymentServices)
                    .ThenInclude(ps => ps.PaymentService)
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize).ToListAsync()
            };

            return result;
        }

        public async Task<Merchant> GetMerchant(string merchantUniqueId)
        {
            return await _context.Merchants
                .Include(m => m.PaymentServices)
                .ThenInclude(ps => ps.PaymentService)
                .Include(m => m.WebStore)
                .FirstOrDefaultAsync(m => m.MerchantUniqueId == merchantUniqueId);
        }

        public async Task<Merchant> GetMerchantByStoreUniqueId(string merchantStoreUniqueId)
        {
            return await _context.Merchants
                .Include(m => m.PaymentServices)
                .ThenInclude(ps => ps.PaymentService)
                .Include(m => m.WebStore)
                .FirstOrDefaultAsync(m => m.MerchantUniqueStoreId == merchantStoreUniqueId);
        }

        public async Task<Merchant> GetMerchantById(Guid id)
        {
            return await _context.Merchants
                .Include(m => m.WebStore)
                .Include(m => m.PaymentServices)
                .ThenInclude(ps => ps.PaymentService)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> AddMerchant(Merchant merchant)
        {
            await _context.Merchants.AddAsync(merchant);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveMerchant(Guid id)
        {
            var merchantToRemove = await _context.Merchants.FirstOrDefaultAsync(m => m.Id == id);
            _context.Merchants.Remove(merchantToRemove);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateMerchant(Guid id, Merchant merchantForUpdate)
        {
            _context.Update(merchantForUpdate);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}