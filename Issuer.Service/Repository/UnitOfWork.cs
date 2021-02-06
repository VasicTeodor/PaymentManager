using Issuer.Service.Data;
using Issuer.Service.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issuer.Service.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IssuerDbContext _context;

        public UnitOfWork(IssuerDbContext context, IAccountRepository accounts, ICardRepository cards, IClientRepository clients, ITransactionRepository transactions, IPaymentRepository payments)
        {
            _context = context;
            Accounts = accounts;
            Cards = cards;
            Clients = clients;
            Transactions = transactions;
            Payments = payments;
        }

        public IAccountRepository Accounts { get; set; }
        public ICardRepository Cards { get; set; }
        public IClientRepository Clients { get; set; }
        public ITransactionRepository Transactions { get; set; }
        public IPaymentRepository Payments { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
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
