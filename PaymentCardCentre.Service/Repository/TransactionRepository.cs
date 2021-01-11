using PaymentCardCentre.Service.Data;
using PaymentCardCentre.Service.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PCCDbContext _context;

        public TransactionRepository(PCCDbContext context)
        {
            _context = context;
        }

        public int AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            return _context.SaveChanges();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
