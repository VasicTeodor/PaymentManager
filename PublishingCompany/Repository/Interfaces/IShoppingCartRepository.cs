using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PublishingCompany.Api.Data.Entities;

namespace PublishingCompany.Api.Repository.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart, Guid>
    {
        Task<IEnumerable<ShoppingCart>> GetAllShoppingCarts();
        Task<ShoppingCart> GetShoppingCartForUser(Guid userId);
    }
}