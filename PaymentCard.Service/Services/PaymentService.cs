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
        private readonly IGenericRestClient _restClient;
        private readonly ISecurityService _securityService;
        private readonly string _pccUrl = "http://localhost:52096/PaymentCardCentre/PersistPayment";

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRestClient restClient, ISecurityService securityService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _restClient = restClient;
            _securityService = securityService;
        }

        public async Task<TransactionDto> SubmitPayment(CardDto cardDto, Guid orderId)
        {
            var card = _unitOfWork.Cards.GetCardByPan(cardDto.Pan);
            var payment = _unitOfWork.Payments.GetPaymentByOrderId(orderId);
            var merchant = _unitOfWork.Clients.Get(payment.Merchant.Id);
            if (payment == null)
            {
                Log.Information($"Payment service submit payment failed");
                return null;
            }


            Transaction transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Merchant = merchant.Account,
                Amount = payment.Amount,
                OrderId = payment.Id,
                Timestamp = DateTime.Now,
            };

            TransactionDto transactionDto = new TransactionDto()
            {
                //videti da li treba otkomentarisati ovo cudo
                AcquirerOrderId = payment.Id,
                AcquirerTimestamp = DateTime.Now,
                Amount = payment.Amount,
                MerchantOrderId = orderId,
                PaymentId = payment.Id
            };


            ///kartica se ne nalazi u banci prodavca
            //var merchantCards = merchant.Account.Cards.ToList();
            //var searchCard = merchantCards.Find(c => c.Pan.Substring(1,6).Equals(cardDto.Pan.Substring(1, 6)));
            if (card==null)
            {
                Log.Information($"Payment service genereting request for PCC");
                var pccClient = _unitOfWork.Clients.FindByName("Pcc");
                if (pccClient == null)
                {
                    Log.Information($"Payment service failed to generate request for PCC");
                    transactionDto.Status = "FAILED";
                    transaction.Status = "FAILED";
                    _unitOfWork.Transactions.Add(transaction);
                    _unitOfWork.Complete();
                    return transactionDto;
                }
                transaction.Payer = pccClient.Account;
                var pccRequest = new PccRequestDto()
                {
                    Amount = transaction.Amount,
                    Timestamp = transaction.Timestamp,
                    CardData = cardDto,
                    OrderId = transaction.OrderId
                };
                var pccResponse = await _restClient.PostRequest<PccResponseDto>(_pccUrl, pccRequest);

                if (pccResponse == null)
                {
                    Log.Information($"Payment service unhandled error occured while working with PCC");
                    transaction.Status = "ERROR";
                    transactionDto.Status = "ERROR";
                    _unitOfWork.Transactions.Add(transaction);
                    _unitOfWork.Complete();
                    return transactionDto;
                }

                if (pccResponse.Status.Equals("SUCCESS"))
                {
                    Log.Information($"Payment service succeded with PCC");
                    merchant = _unitOfWork.Clients.Get(payment.Merchant.Id);
                    merchant.Account.Amount += payment.Amount;
                    _unitOfWork.Clients.Update(merchant);
                    payment.Status = pccResponse.Status;
                    _unitOfWork.Payments.Update(payment);
                    transaction.Status = "SUCCESS";
                    transactionDto.Status = "SUCCESS";
                    _unitOfWork.Transactions.Add(transaction);
                    _unitOfWork.Complete();
                    return transactionDto;
                }
                else
                {
                    Log.Information($"Payment service failed with PCC");
                    transaction.Status = "ERROR";
                    transactionDto.Status = "ERROR";
                    _unitOfWork.Transactions.Add(transaction);
                    _unitOfWork.Complete();
                    return transactionDto;
                }
            }
            else
            {
                //prodavac i kupac se nalaze u istoj banci
                //pogledati kasnije da li treba otkomentarisati ovo cudo
                //postavi payera ovde odmah jer je payer u istoj banci kao i mechant kasnije prepraviti
                //Transaction transaction = new Transaction()
                //{
                //    Id = Guid.NewGuid(),
                //    Merchant = merchant.Account,
                //    Payer = payer.Account,
                //    Amount = payment.Amount,
                //    OrderId = payment.Id,
                //    Timestamp = DateTime.Now,
                //};

                //TransactionDto transactionDto = new TransactionDto()
                //{
                //    AcquirerOrderId = payer.Id,
                //    AcquirerTimestamp = DateTime.Now,
                //    Amount = payment.Amount,
                //    MerchantOrderId = orderId,
                //    PaymentId = payment.Id
                //};
                var payer = _unitOfWork.Clients.Get(card.Account.Id);

                //dodati i proveru datuma kasnije 
                //var kkkkk = _securityService.DecryptStringAes(card.SecurityCode);
                if (!card.SecurityCode.Equals(cardDto.SecurityCode) || !card.HolderName.Equals(cardDto.HolderName))
                {
                    transaction.Status = "ERROR";
                    transactionDto.Status = "ERROR";
                    Log.Information($"Payment service transaction error");
                    return transactionDto;
                }

                //nema dovoljno sredstava
                if (payer.Account.Amount < payment.Amount)
                {
                    transaction.Status = "FAILED";
                    transactionDto.Status = "FAILED";
                    Log.Information($"Payment service transaction failed");
                }
                else
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
