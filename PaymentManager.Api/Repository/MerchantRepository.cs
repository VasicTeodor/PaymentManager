﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Data;
using PaymentManager.Api.Helpers;
using PaymentManager.Api.Repository.Interfaces;
using PaymentManager.Api.Services.Interfaces;

namespace PaymentManager.Api.Repository
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly DataContext _context;
        private readonly ISecurityCryptographyService _cryptographyService;

        public MerchantRepository(DataContext context, ISecurityCryptographyService cryptographyService)
        {
            this._context = context;
            _cryptographyService = cryptographyService;
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

        public async Task<Merchant> GetMerchant(string merchantUniqueId, bool decrypt = false)
        {
            var chipMerchantUniqueId = _cryptographyService.EncryptStringAes(merchantUniqueId);
            var merchant = await _context.Merchants
                .Include(m => m.PaymentServices)
                .ThenInclude(ps => ps.PaymentService)
                .Include(m => m.WebStore)
                .FirstOrDefaultAsync(m => m.MerchantUniqueId == chipMerchantUniqueId);

            if (merchant != null && decrypt)
            {
                merchant.MerchantUniqueId = _cryptographyService.DecryptStringAes(merchant.MerchantUniqueId);
                merchant.MerchantPassword = _cryptographyService.DecryptStringAes(merchant.MerchantPassword);
            }

            return merchant;
        }

        public async Task<Merchant> GetMerchantByStoreUniqueId(string merchantStoreUniqueId, bool decrypt = false)
        {
            var merchant = await _context.Merchants
                .Include(m => m.PaymentServices)
                .ThenInclude(ps => ps.PaymentService)
                .Include(m => m.WebStore)
                .FirstOrDefaultAsync(m => m.MerchantUniqueStoreId == merchantStoreUniqueId);

            if (merchant != null && decrypt)
            {
                merchant.MerchantUniqueId = _cryptographyService.DecryptStringAes(merchant.MerchantUniqueId);
                merchant.MerchantPassword = _cryptographyService.DecryptStringAes(merchant.MerchantPassword);
            }

            return merchant;
        }

        public async Task<Merchant> GetMerchantById(Guid id, bool decrypt = false)
        {
            var merchant = await _context.Merchants
                .Include(m => m.WebStore)
                .Include(m => m.PaymentServices)
                .ThenInclude(ps => ps.PaymentService)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (merchant != null && decrypt)
            {
                merchant.MerchantUniqueId = _cryptographyService.DecryptStringAes(merchant.MerchantUniqueId);
                merchant.MerchantPassword = _cryptographyService.DecryptStringAes(merchant.MerchantPassword);
            }

            return merchant;
        }

        public async Task<bool> AddMerchant(Merchant merchant)
        {
            merchant.MerchantUniqueId = _cryptographyService.EncryptStringAes(merchant.MerchantUniqueId);
            merchant.MerchantPassword = _cryptographyService.EncryptStringAes(merchant.MerchantPassword);
            var webStore = await _context.WebStores.FirstOrDefaultAsync(ws => ws.Id == merchant.WebStore.Id);
            merchant.WebStore = webStore;
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
            merchantForUpdate.MerchantUniqueId = _cryptographyService.EncryptStringAes(merchantForUpdate.MerchantUniqueId);
            merchantForUpdate.MerchantPassword = _cryptographyService.EncryptStringAes(merchantForUpdate.MerchantPassword);
            _context.Update(merchantForUpdate);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}