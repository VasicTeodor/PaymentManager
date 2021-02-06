using Issuer.Service.Data;
using Issuer.Service.Data.Entities;
using Issuer.Service.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issuer.Service.Repository.Implementations
{
    public class PaymentRepository : Repository<Payment, Guid>, IPaymentRepository
    {
        private readonly IssuerDbContext _context;

        public PaymentRepository(IssuerDbContext context) : base(context)
        {
            this._context = context;
        }

        public Payment GetPaymentByOrderId(Guid orderId)
        {
            return _context.Payments.Include(p => p.Merchant).ThenInclude(p => p.Cards).FirstOrDefault(p => p.Id.Equals(orderId));
        }

        public Payment GetPaymentByUrl(string url)
        {
            return _context.Payments.Include(p => p.Merchant).ThenInclude(p => p.Cards).FirstOrDefault(p => p.Url.Equals(url));
        }
    }
}
