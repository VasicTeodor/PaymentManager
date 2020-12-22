using Bank.Service.Data;
using Bank.Service.Data.Entities;
using Bank.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bank.Service.Repositories.Implementations
{
    public class ClientRepository : Repository<Client, Guid>, IClientRepository
    {
        private readonly BankDbContext _context;
        public ClientRepository(BankDbContext context) : base(context)
        {
            this._context = context;
        }

        public Client FindByPayerId(string merchantId)
        {
            return _context.Clients.Include(c => c.Account).FirstOrDefault(x => x.MerchantId.Equals(merchantId));
        }
    }
}
