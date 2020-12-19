using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Dtos;
using PaymentManager.Api.Models;
using PaymentManager.Api.Repository.Interfaces;
using PaymentManager.Api.Services.Interfaces;
using Serilog;
using PaymentRequest = PaymentManager.Api.Models.PaymentRequest;

namespace PaymentManager.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPaymentRequestRepository _paymentRequestRepository;
        private readonly IMapper _mapper;

        public PaymentService(IMerchantRepository merchantRepository, ITransactionRepository transactionRepository, IPaymentRequestRepository paymentRequestRepository, IMapper mapper)
        {
            _merchantRepository = merchantRepository;
            _transactionRepository = transactionRepository;
            _paymentRequestRepository = paymentRequestRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates payment request for merchants bank
        /// </summary>
        /// <param name="merchantId">unique id for merchant</param>
        /// <param name="amount">money amount for transaction</param>
        /// <returns></returns>
        public async Task<PaymentRequest> GeneratePaymentRequest(string merchantId, decimal amount)
        {
            var merchant = await _merchantRepository.GetMerchantByStoreUniqueId(merchantId);

            var paymentRequest = new PaymentRequest
            {
                MerchantId = merchant.MerchantUniqueId,
                MerchantPassword = merchant.MerchantPassword,
                SuccessUrl = merchant.WebStore.SuccessUrl,
                ErrorUrl = merchant.WebStore.ErrorUrl,
                FailedUrl = merchant.WebStore.FailureUrl,
                MerchantOrderId = Guid.NewGuid(),
                Amount = amount,
                MerchantTimestamp = DateTime.UtcNow
            };

            Log.Information($"PAYMENT REQUEST CREATED: {paymentRequest}");

            var paymentRequestDb = _mapper.Map<Data.Entities.PaymentRequest>(paymentRequest);

            paymentRequestDb.MerchantId = merchant.Id;
            await _paymentRequestRepository.SaveRequest(paymentRequestDb);

            return paymentRequest;
        }

        /// <summary>
        /// Finds merchant, then web store and urls for finalizing transaction, after that by status of transaction redirects user to suitable url
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<CompleteTransactionResult> CompleteTransaction(Transaction transaction)
        {
            Log.Information($"FINISHING TRANSACTION: {transaction}");
            var paymentRequest =
                await _paymentRequestRepository.GetRequestByMerchantOrderId(transaction.MerchantOrderId);

            var merchant = await _merchantRepository.GetMerchantById(paymentRequest.MerchantId);

            var isSaved = await _transactionRepository.SaveTransaction(transaction);

            var result = new CompleteTransactionResult()
            {
                IsSaved = isSaved,
                UrlForRedirection = ReturnSuitableUrl(transaction.Status, merchant.WebStore)
            };

            return result;
        }

        public async Task<List<PaymentServiceDto>> GetPaymentOptions(string merchantStoreId)
        {
            Log.Information($"Getting payment options for merchant: {merchantStoreId}");
            var merchant = await _merchantRepository.GetMerchantByStoreUniqueId(merchantStoreId);
            var paymentServices = merchant.PaymentServices.Select(ps => ps.PaymentService).ToList();
            var paymentOptions = _mapper.Map<List<PaymentServiceDto>>(paymentServices);

            return paymentOptions;
        }


        private string ReturnSuitableUrl(string transactionStatus, WebStore webStore)
        {
            string url;
            switch (transactionStatus)
            {
                case "SUCCESS":
                    url = webStore.SuccessUrl;
                    break;
                case "FAILED":
                    url = webStore.FailureUrl;
                    break;
                default:
                    url = webStore.ErrorUrl;
                    break;
            }

            return url;
        }
    }
}