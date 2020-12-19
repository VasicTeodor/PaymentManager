using System.Threading.Tasks;
using PaymentManager.Api.Data;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Repository.Interfaces;

namespace PaymentManager.Api.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;

        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveTransaction(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);

            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}