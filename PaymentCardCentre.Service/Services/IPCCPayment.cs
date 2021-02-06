using PaymentCardCentre.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Services
{
    public interface IPCCPayment
    {
        Task<ResponseDto> Payment(RequestDto request);
    }
}
