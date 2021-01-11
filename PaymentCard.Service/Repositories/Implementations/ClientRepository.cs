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

        public Client GetClientWithNavigationProperties(Guid id)
        {
            return _context.Clients.Include(c => c.Account).ThenInclude(c => c.Cards).FirstOrDefault(x => x.Id.Equals(id));
        }

        public Client FindByName(string firstName)
        {
            return _context.Clients.Include(c => c.Account).FirstOrDefault(x => x.FirstName.Equals(firstName));
        }

        public Client FindByPayerId(string merchantId)
        {
            return _context.Clients.Include(c => c.Account).FirstOrDefault(x => x.MerchantId.Equals(merchantId));
        }
    }
}
