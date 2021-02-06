using Issuer.Service.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issuer.Service.Repository
{
    public interface IUnitOfWork
    {
        IAccountRepository Accounts { get; set; }
        ICardRepository Cards { get; set; }
        IClientRepository Clients { get; set; }
        ITransactionRepository Transactions { get; set; }
        IPaymentRepository Payments { get; set; }
        int Complete();
        Task<int> CompleteAsync();
    }
}
