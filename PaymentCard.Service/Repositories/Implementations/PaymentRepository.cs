using Bank.Service.Data;
using Bank.Service.Data.Entities;
using Bank.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Repositories.Implementations
{
    public class PaymentRepository : Repository<Payment, Guid>, IPaymentRepository
    {
        private readonly BankDbContext _context;

        public PaymentRepository(BankDbContext context) : base(context)
        {
            this._context = context;
        }

        public Payment GetPaymentByOrderId(Guid orderId)
        {
            return _context.Payments.Where(p => p.Id.Equals(orderId)).FirstOrDefault();
        }

        public Payment GetPaymentByUrl(string url)
        {
            return _context.Payments.Where(p => p.Url.Equals(url)).FirstOrDefault();
        }
    }
}
