using System.Threading.Tasks;
using PaymentManager.Api.Data.Entities;

namespace PaymentManager.Api.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        Task<bool> SaveTransaction(Transaction transaction);
    }
}