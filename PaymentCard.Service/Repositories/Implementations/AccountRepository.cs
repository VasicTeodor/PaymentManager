using Bank.Service.Data;
using Bank.Service.Data.Entities;
using Bank.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Repositories.Implementations
{
    public class AccountRepository : Repository<Account, Guid>, IAccountRepository
    {
        private readonly BankDbContext _context;

        public AccountRepository(BankDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
