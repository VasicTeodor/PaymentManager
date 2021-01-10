using BitCoin.Service.Models;
using BitCoin.Service.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoin.Service.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BitCoinContext _context;

        public OrderRepository(BitCoinContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveOrder(Order order)
        {
            await _context.Order.AddAsync(order);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Order> GetOrder(string orderId)
        {
            return await _context.Order.FirstOrDefaultAsync(r => r.OrderId == orderId);
        }

        public async Task<bool> SaveOrderResult(OrderResult orderResult)
        {
            await _context.OrderResult.AddAsync(orderResult);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<OrderResult> GetOrderResult(string orderResultId)
        {
            return await _context.OrderResult.FirstOrDefaultAsync(r => r.OrderId == orderResultId);
        }
    }
}
