using PaymentManager.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentManager.Api.Repository.Interfaces
{
    interface IWebStoreMerchant
    {
        Task<bool> AddMerchantToWebStore(WebStore webStore, Merchant merchant);
        Task<bool> RemoveMerchantFromWebStore(WebStore webStore, Merchant merchant);
    }
}
