using Issuer.Service.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issuer.Service.Repository.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
    }
}
