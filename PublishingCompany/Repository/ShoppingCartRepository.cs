using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PublishingCompany.Api.Data;
using PublishingCompany.Api.Data.Entities;
using PublishingCompany.Api.Repository.Interfaces;

namespace PublishingCompany.Api.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart, Guid>, IShoppingCartRepository
    {
        private readonly DataContext _context;

        public ShoppingCartRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllShoppingCarts()
        {
            return await _context.ShoppingCarts.Include(shp => shp.ShoppingCartItems).ThenInclude(shcit => shcit.Book)
                .Include(shp => shp.User).ToListAsync();
        }

        public async Task<ShoppingCart> GetShoppingCartForUser(Guid userId)
        {
            return await _context.ShoppingCarts.Include(shp => shp.ShoppingCartItems).ThenInclude(shcit => shcit.Book)
                .Include(shp => shp.User).FirstOrDefaultAsync(shp => shp.User.Id == userId);
        }
    }
}