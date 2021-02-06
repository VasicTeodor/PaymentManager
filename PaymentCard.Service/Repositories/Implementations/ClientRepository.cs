using Bank.Service.Data;
using Bank.Service.Data.Entities;
using Bank.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bank.Service.Services;

namespace Bank.Service.Repositories.Implementations
{
    public class ClientRepository : Repository<Client, Guid>, IClientRepository
    {
        private readonly BankDbContext _context;
        private readonly ISecurityService _securityService;
        public ClientRepository(BankDbContext context, ISecurityService securityService) : base(context)
        {
            this._context = context;
            this._securityService = securityService;
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
            var clients = _context.Clients.Include(c => c.Account).ThenInclude(c=>c.Client).ToList();
            foreach (var client in clients)
            {
                if (client.MerchantId == null)
                    continue;
                var restId = _securityService.DecryptStringAes(client.MerchantId);
                if (restId.Equals(merchantId))
                {
                    return client;
                }
            }
            return null;
            //return _context.Clients.Include(c => c.Account).FirstOrDefault(x => x.MerchantId.Equals(merchantId));
        }
    }
}
