using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoin.Service.Models
{
    public class BitCoinContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderResult> OrderResult { get; set; }

        public BitCoinContext(DbContextOptions<BitCoinContext> options) : base(options)
        {

        }
    }
}
