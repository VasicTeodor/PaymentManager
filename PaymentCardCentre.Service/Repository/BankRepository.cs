using PaymentCardCentre.Service.Data;
using PaymentCardCentre.Service.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly PCCDbContext _context;

        public BankRepository(PCCDbContext context)
        {
            _context = context;
        }

        public int AddBankByPan(string pan)
        {
            _context.Banks.Add(new Bank() { Id = Guid.NewGuid(), Pan = pan });
            return _context.SaveChanges();
        }

        public Bank GetBankByPan(string panPart)
        {
            return _context.Banks.Where(z => z.Pan.Equals(panPart)).FirstOrDefault();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
