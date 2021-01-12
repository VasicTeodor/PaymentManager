using Issuer.Service.Data;
using Issuer.Service.Data.Entities;
using Issuer.Service.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issuer.Service.Repository.Implementations
{
    public class TransactionRepository : Repository<Transaction, Guid>, ITransactionRepository
    {
        private readonly IssuerDbContext _context;

        public TransactionRepository(IssuerDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
