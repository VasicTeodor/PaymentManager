using PaymentCardCentre.Service.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Repository
{
    public interface IBankRepository
    {
        Bank GetBankByPan(string panPart);
        Task<int> AddBankByPan(string pan);
        int SaveChanges();
        Task<int> SaveChangeAsync();
    }
}
