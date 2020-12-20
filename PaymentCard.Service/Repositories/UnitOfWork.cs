using Bank.Service.Data;
using Bank.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankDbContext _context;

        public UnitOfWork(BankDbContext context, IClientRepository accounts, ICardRepository cards, IClientRepository clients, ITransactionRepository transactions)
        {
            _context = context;
            Accounts = accounts;
            Cards = cards;
            Clients = clients;
            Transactions = transactions;
        }

        public IClientRepository Accounts { get; set; }
        public ICardRepository Cards { get; set; }
        public IClientRepository Clients { get; set; }
        public ITransactionRepository Transactions { get; set; }

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
