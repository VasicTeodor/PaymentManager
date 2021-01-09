using BitCoin.Service.Dtos;
using BitCoin.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoin.Service.Services.Interfaces
{
    public interface ICoingateService
    {
        Task<dynamic> CreatePayment(OrderDto order);
    }
}
