using Bank.Service.Data;
using Bank.Service.Data.Entities;
using Bank.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            return _context.Payments.Include(p => p.Merchant).ThenInclude(p=>p.Cards).FirstOrDefault(p => p.Id.Equals(orderId));
        }

        public Payment GetPaymentByUrl(string url)
        {
            return _context.Payments.Include(p => p.Merchant).ThenInclude(p=>p.Cards).FirstOrDefault(p => p.Url.Equals(url));
        }
    }
}
