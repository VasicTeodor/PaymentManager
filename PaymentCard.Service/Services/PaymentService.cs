using Bank.Service.Dto;
using Bank.Service.Models;
using Bank.Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool SubmitPayment(CardDto cardDto)
        {
            var card = _unitOfWork.Cards.GetCardByPan(cardDto.Pan);

            if (card != null)
            {
                return false;
            }
            return true;
        }

        public PaymentRequestResponseDto ValidatePayment(PaymentRequest request)
        {
            var responseDto = new PaymentRequestResponseDto();
            var merch = _unitOfWork.Clients.FindByPayerId(request.MerchantId);
            //promeniti na https i videti da li je dovoljna promena da li korisnik postoji da je zahtev validan
            if (merch != null)
            {
                responseDto.PaymentUrl = "http://localhost:4200/";
            }else
            {
                responseDto.PaymentUrl = "http://localhost:4200/";
                responseDto.PaymentId = Guid.NewGuid();
            }
            return responseDto;
        }
    }
}
