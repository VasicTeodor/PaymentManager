﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using PaymentManager.Api.Data;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Helpers;
using PaymentManager.Api.Repository.Interfaces;

namespace PaymentManager.Api.Repository
{
    public class WebStoreRepository : IWebStoreRepository
    {
        private readonly DataContext _context;

        public WebStoreRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<WebStore> GetWebStoreByIdAndName(Guid id, string name)
        {
            return await _context.WebStores.FirstOrDefaultAsync(ws => ws.Id == id && ws.StoreName.ToLower() == name.ToLower());
        }

        public async Task<bool> AddWebStore(WebStore webStore)
        {
            await _context.WebStores.AddAsync(webStore);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<WebStore> GetWebStoreById(Guid id)
        {
            return await _context.WebStores.Include(ws => ws.PaymentOptions).ThenInclude(po => po.PaymentService).FirstOrDefaultAsync(ws => ws.Id == id);
        }

        public async Task<PaginationResult<WebStore>> GetWebStores(int pageNumber = 1, int pageSize = 10)
        {
            var result = new PaginationResult<WebStore>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                NumberOfItems = await _context.WebStores.CountAsync(),
                Items = await _context.WebStores.Include(ws => ws.PaymentOptions).ThenInclude(ws => ws.PaymentService).Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize).ToListAsync()
            };

            return result;
        }

        public async Task<bool> RemoveWebStore(Guid id)
        {
            var webStoreToRemove = await _context.WebStores.Include(ws => ws.PaymentOptions)
                .Include(ws => ws.Merchants).FirstOrDefaultAsync(WebStore => WebStore.Id == id);
            _context.WebStores.Remove(webStoreToRemove);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateWebStore(Guid id, WebStore webStoreForUpdate)
        {
            _context.WebStores.Update(webStoreForUpdate);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
