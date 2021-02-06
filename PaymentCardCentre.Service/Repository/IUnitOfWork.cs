using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ITransactionRepository Transactions { get; set; }
        IBankRepository Banks { get; set; }
        int Complete();
    }
}
