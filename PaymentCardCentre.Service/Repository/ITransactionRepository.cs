using PaymentCardCentre.Service.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Repository
{
    public interface ITransactionRepository
    {
        int AddTransaction(Transaction transaction);
        int SaveChanges();
    }
}
