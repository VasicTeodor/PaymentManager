using AutoMapper;
using Bank.Service.Data.Entities;
using Bank.Service.Dto;
using Bank.Service.Models;
using Bank.Service.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public TransactionDto SubmitPayment(CardDto cardDto, Guid orderId)
        {
            var card = _unitOfWork.Cards.GetCardByPan(cardDto.Pan);
            var payment = _unitOfWork.Payments.GetPaymentByOrderId(orderId);
            var payer = _unitOfWork.Clients.Get(card.Account.Id);
            var merchant = _unitOfWork.Clients.Get(payment.Merchant.Id);
            if (card == null || payment == null || payer == null || merchant == null)
            {
                Log.Information($"Payment service submit payment failed");
                return null;
            }

            //postavi payera ovde odmah jer je payer u istoj banci kao i mechant kasnije prepraviti
            Transaction transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Merchant = merchant.Account,
                Payer = payer.Account,
                Amount = payment.Amount,
                OrderId = payment.Id,
                Timestamp = DateTime.Now,
            };

            TransactionDto transactionDto = new TransactionDto()
            {
                AcquirerOrderId = payer.Id,
                AcquirerTimestamp = DateTime.Now,
                Amount = payment.Amount,
                MerchantOrderId = orderId,
                PaymentId = payment.Id
            };

            //dodati i proveru datuma kasnije 
            if (card.SecurityCode != cardDto.SecurityCode || !card.HolderName.Equals(cardDto.HolderName))
            {
                transaction.Status = "ERROR";
                transactionDto.Status = "ERROR";
                Log.Information($"Payment service transaction error");
                return transactionDto;
            }

            //nema dovoljno sredstava
            if(payer.Account.Amount < payment.Amount)
            {
                transaction.Status = "FAILED";
                transactionDto.Status = "FAILED";
                Log.Information($"Payment service transaction failed");
            } else
            {
                merchant.Account.Amount += payment.Amount;
                payer.Account.Amount -= payment.Amount;
                _unitOfWork.Clients.Update(merchant);
                _unitOfWork.Clients.Update(payer);
                _unitOfWork.Complete();
                transaction.Status = "SUCCESS";
                transactionDto.Status = "SUCCESS";
                Log.Information($"Payment service transaction success");
            }
            _unitOfWork.Transactions.Add(transaction);
            _unitOfWork.Complete();

            return transactionDto;
        }

        public PaymentRequestResponseDto ValidatePayment(PaymentRequest request)
        {
            Log.Information($"Bank service generating payment response");
            var responseDto = new PaymentRequestResponseDto();
            var merchant = _unitOfWork.Clients.FindByPayerId(request.MerchantId);
            //promeniti na https i videti da li je dovoljna promena da li korisnik postoji da je zahtev validan
            //url ce da bude /:orderId
            //inace vraca prazan sajt
            //napraviti payment objekat i odatle sve vuci na na kp back
            var payment = new Payment() {
                Id = request.MerchantOrderId,
                Amount = request.Amount, 
                Merchant=merchant.Account, 
                Status="PAYMENT_CREATED", 
                SuccessUrl = request.SuccessUrl,
                ErrorUrl = request.ErrorUrl,
                FailedUrl = request.FailedUrl,
                Url = "http://localhost:4201/"
            };
            try
            {
                _unitOfWork.Payments.Add(payment);
                _unitOfWork.Complete();
            }catch(Exception e)
            {
                Log.Information($"Bank service failed to add payment {payment.ToString()} reason {e.Message}");
            }
            Log.Information($"Bank service payment created {payment.ToString()}");

            if (merchant != null)
            {
                Log.Information($"Bank service succesfully generated payment response returning payment Url and Id to Payment manager");
                responseDto.PaymentUrl = "http://localhost:4201/" + request.MerchantOrderId;
                responseDto.PaymentId = payment.Id;
            }
            else
            {
                Log.Information($"Bank service failed to generate payment response returning payment Url and Id to Payment manager");
                responseDto.PaymentUrl = "http://localhost:4201/";
                responseDto.PaymentId = Guid.Empty;
            }
            return responseDto;
        }
    }
}
