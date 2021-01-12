using Issuer.Service.Data;
using Issuer.Service.Data.Entities;
using Issuer.Service.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issuer.Service.Repository.Implementations
{
    public class AccountRepository : Repository<Account, Guid>, IAccountRepository
    {
        private readonly IssuerDbContext _context;

        public AccountRepository(IssuerDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
