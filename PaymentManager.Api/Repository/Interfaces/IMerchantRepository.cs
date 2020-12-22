using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentManager.Api.Helpers;
using PaymentManager.Api.Data.Entities;

namespace PaymentManager.Api.Repository.Interfaces
{
    public interface IMerchantRepository
    {
        Task<PaginationResult<Merchant>> GetMerchants(int pageNumber = 1, int pageSize = 10);
        Task<Merchant> GetMerchant(string merchantUniqueId, bool decrypt = false);
        Task<Merchant> GetMerchantByStoreUniqueId(string merchantStoreUniqueId, bool decrypt = false);
        Task<Merchant> GetMerchantById(Guid id, bool decrypt = false);
        Task<bool> AddMerchant(Merchant merchant);
        Task<bool> RemoveMerchant(Guid id);
        Task<bool> UpdateMerchant(Guid id, Merchant merchantForUpdate);
    }
}