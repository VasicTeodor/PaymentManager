using Bank.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Services
{
    public interface IIssuerPaymentService
    {
        ResponseDto IssuerPayment(RequestDto request);
    }
}
