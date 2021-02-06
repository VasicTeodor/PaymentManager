using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PublishingCompany.Api.Data;
using PublishingCompany.Api.Data.Entities;
using PublishingCompany.Api.Repository.Interfaces;

namespace PublishingCompany.Api.Repository
{
    public class OrderRepository : Repository<Order, Guid>, IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders.Include(order => order.OrderItems).ThenInclude(orderitems => orderitems.Book)
                .ToListAsync();
        }

        public async Task<bool> AddNewOrder(Order newOrder)
        {
            await  _context.Orders.AddAsync(newOrder);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Order> GetOrderWithItems(Guid orderId)
        {
            return await _context.Orders.Include(or => or.OrderItems).ThenInclude(it => it.Book)
                .FirstOrDefaultAsync(order => order.Id == orderId);
        }
    }
}