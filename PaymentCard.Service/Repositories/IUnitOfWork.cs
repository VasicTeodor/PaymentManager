using Bank.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Accounts { get; set; }
        ICardRepository Cards { get; set; }
        IClientRepository Clients { get; set; }
        ITransactionRepository Transactions { get; set; }
        IPaymentRepository Payments { get; set; }
        int Complete();
        Task<int> CompleteAsync();
    }
}
