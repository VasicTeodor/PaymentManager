using PaymentCardCentre.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PCCDbContext _context;

        public UnitOfWork(PCCDbContext context, ITransactionRepository transactions, IBankRepository banks)
        {
            _context = context;
            Transactions = transactions;
            Banks = banks;
        }

        public ITransactionRepository Transactions { get; set; }
        public IBankRepository Banks { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
