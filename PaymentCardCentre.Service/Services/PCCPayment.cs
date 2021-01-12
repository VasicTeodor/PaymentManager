using PaymentCardCentre.Service.Data.Entities;
using PaymentCardCentre.Service.Dto;
using PaymentCardCentre.Service.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCentre.Service.Services
{
    public class PCCPayment : IPCCPayment
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRestClient restClient;
        private readonly string _issuerUrl = "http://localhost:55208/Issuer/IssuerPayment";

        public PCCPayment(IUnitOfWork unitOfWork, IGenericRestClient restClient)
        {
            this.unitOfWork = unitOfWork;
            this.restClient = restClient;
        }

        public async Task<ResponseDto> Payment(RequestDto request)
        {
            Log.Information($"PCC Payment request started");
            Transaction transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Amount = request.Amount,
                AcquierOrderId = request.OrderId,
                AcquierTimestamp = request.Timestamp,
            };

            ResponseDto response = new ResponseDto() 
            { 
                AcquierOrderId = request.OrderId, 
                AcquierTimestamp = request.Timestamp 
            };

            //izvuci deo za banku da se raspozna
            string panPart = request.CardData.Pan.Substring(1, 6);
            var bank = unitOfWork.Banks.GetBankByPan(panPart);
            if (bank == null)
            {
                Log.Information($"PCC Payment request failed, no bank found");
                transaction.Status = "ERROR";
                response.Status = "ERROR";
                unitOfWork.Complete();
                return response;
            }

            var issuerResponseDto = await restClient.PostRequest<ResponseDto>(_issuerUrl, request);

            if (issuerResponseDto == null)
            {
                Log.Information($"PCC payment Issuer request failed");
                transaction.Status = "FAILED";
                response.Status = "FAILED";
                return response;
            }

            Log.Information($"PCC payment Issuer request succeded");
            transaction.IssuerOrderId = issuerResponseDto.IssuerOrderId;
            transaction.IssuerTimestamp = issuerResponseDto.IssuerTimestamp;
            transaction.Status = "SUCCESS";
            unitOfWork.Transactions.AddTransaction(transaction);
            unitOfWork.Complete();

            response.IssuerOrderId = issuerResponseDto.IssuerOrderId;
            response.IssuerTimestamp = issuerResponseDto.IssuerTimestamp;
            response.Status = "SUCCESS";
            return response;
        }
    }
}
