using Bank.Service.Data;
using Bank.Service.Data.Entities;
using Bank.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Repositories.Implementations
{
    public class TransactionRepository : Repository<Transaction, Guid>, ITransactionRepository
    {
        private readonly BankDbContext _context;

        public TransactionRepository(BankDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
