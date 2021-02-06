using PaymentCardCentre.Service.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Data
{
    public class DbSeed
    {
        private readonly PCCDbContext _context;

        public DbSeed(PCCDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public void SeedBank()
        {
            if (!_context.Banks.Any())
            {
                var bank = new Bank() { Id = Guid.NewGuid(), Pan = "131225" };
                _context.Banks.Add(bank);
                _context.SaveChanges();
            }
        }
    }
}
