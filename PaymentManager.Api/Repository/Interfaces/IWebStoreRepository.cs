using System;
using System.Threading.Tasks;
using PaymentManager.Api.Helpers;
using PaymentManager.Api.Data.Entities;

namespace PaymentManager.Api.Repository.Interfaces
{
    public interface IWebStoreRepository
    {
        Task<PaginationResult<WebStore>> GetWebStores(int pageNumber = 1, int pageSize = 10);
        Task<WebStore> GetWebStoreById(Guid id);
        Task<bool> AddWebStore(WebStore webStore);
        Task<bool> RemoveWebStore(Guid id);
        Task<bool> UpdateWebStore(Guid id, WebStore webStoreForUpdate);
    }
}