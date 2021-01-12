using Issuer.Service.Data;
using Issuer.Service.Data.Entities;
using Issuer.Service.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issuer.Service.Repository.Implementations
{
    public class ClientRepository : Repository<Client, Guid>, IClientRepository
    {
        private readonly IssuerDbContext _context;
        public ClientRepository(IssuerDbContext context) : base(context)
        {
            this._context = context;
        }

        public Client GetClientWithNavigationProperties(Guid id)
        {
            return _context.Clients.Include(c => c.Account).ThenInclude(c => c.Client).FirstOrDefault(x => x.Id.Equals(id));
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
