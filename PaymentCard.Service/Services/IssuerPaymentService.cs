using Bank.Service.Data.Entities;
using Bank.Service.Dto;
using Bank.Service.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Services
{
    public class IssuerPaymentService : IIssuerPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISecurityService _securityService;

        public IssuerPaymentService(IUnitOfWork unitOfWork, ISecurityService securityService)
        {
            _unitOfWork = unitOfWork;
            _securityService = securityService;
        }

        public ResponseDto IssuerPayment(RequestDto request)
        {
            ResponseDto response = new ResponseDto();
            Transaction transaction = new Transaction()
            {
                Amount = request.Amount,
                Id = Guid.NewGuid(),
                OrderId = request.OrderId,
                Timestamp = request.Timestamp,
            };
            var card = _unitOfWork.Cards.GetCardByPan(request.CardData.Pan);
            var payer = _unitOfWork.Clients.GetClientWithNavigationProperties(card.Account.Client.Id);
            var pccClient = _unitOfWork.Clients.FindByName("Pcc");
            if (pccClient == null)
            {
                Log.Information($"Issuer Payment service failed to find PCC");
                response.Status = "FAILED";
                transaction.Status = "FAILED";
                _unitOfWork.Transactions.Add(transaction);
                _unitOfWork.Complete();
                return response;
            }

            if (!_securityService.DecryptStringAes(card.SecurityCode).Equals(request.CardData.SecurityCode) || !card.HolderName.Equals(request.CardData.HolderName))
            {
                transaction.Status = "ERROR";
                response.Status = "ERROR";
                Log.Information($"Issuer payment service transaction error");
                _unitOfWork.Transactions.Add(transaction);
                _unitOfWork.Complete();
                return response;
            }

            transaction.Merchant = pccClient.Account;
            transaction.Payer = payer.Account;

            if(payer.Account.Amount < request.Amount)
            {
                //nema sredstava
                Log.Information($"Issuer payment service error, not enough amount on card");
                transaction.Status = "ERROR";
                response.Status = "ERROR";
                _unitOfWork.Transactions.Add(transaction);
                _unitOfWork.Complete();
                return response;
            } else
            {
                Log.Information($"Issuer payment service success");
                transaction.Status = "SUCCESS";
                response.Status = "SUCCESS";
                payer.Account.Amount -= request.Amount;
                _unitOfWork.Clients.Update(payer);
                _unitOfWork.Transactions.Add(transaction);
                _unitOfWork.Complete();
                response.IssuerOrderId = transaction.OrderId;
                response.IssuerTimestamp = transaction.Timestamp;
                return response;
            }
        }
    }
}
