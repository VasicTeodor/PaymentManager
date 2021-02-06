using Microsoft.EntityFrameworkCore;
using PaymentCardCentre.Service.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Data
{
    public class PCCDbContext : DbContext
    {
        public PCCDbContext(DbContextOptions<PCCDbContext> options) : base(options)
        {

        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
