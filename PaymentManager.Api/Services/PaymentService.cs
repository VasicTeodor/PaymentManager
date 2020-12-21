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
        /// <param name="amount">money amount for transaction</param>
        /// <param name="orderId">order id from consumer application</param>
        /// <returns></returns>
        public async Task<PaymentRequest> GeneratePaymentRequest(decimal amount, Guid orderId)
        {
            var paymentRequestFromApp = await _paymentRequestRepository.GetRequestByMerchantOrderId(orderId);
            var merchant = await _merchantRepository.GetMerchantById(paymentRequestFromApp.MerchantId);

            var paymentRequest = new PaymentRequest
            {
                MerchantId = merchant.MerchantUniqueId,
                MerchantPassword = merchant.MerchantPassword,
                SuccessUrl = merchant.WebStore.SuccessUrl,
                ErrorUrl = merchant.WebStore.ErrorUrl,
                FailedUrl = merchant.WebStore.FailureUrl,
                MerchantOrderId = paymentRequestFromApp.Id,
                Amount = amount == 0? paymentRequestFromApp.Amount : amount,
                MerchantTimestamp = paymentRequestFromApp.MerchantTimestamp
            };

            Log.Information($"PAYMENT REQUEST CREATED: {paymentRequest}");

            //var paymentRequestDb = _mapper.Map<Data.Entities.PaymentRequest>(paymentRequest);

            //paymentRequestDb.MerchantId = merchant.Id;
            //await _paymentRequestRepository.SaveRequest(paymentRequestDb);

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

        /// <summary>
        /// Returns payment request for order Id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<Data.Entities.PaymentRequest> GetPaymentRequestDetails(Guid orderId)
        {
            return await _paymentRequestRepository.GetRequestByPaymentRequestId(orderId);
        }

        /// <summary>
        /// Gets payment options for merchant id
        /// </summary>
        /// <param name="merchantStoreId"></param>
        /// <returns></returns>
        public async Task<List<PaymentServiceDto>> GetPaymentOptions(Guid merchantStoreId)
        {
            Log.Information($"Getting payment options for merchant: {merchantStoreId}");
            var merchant = await _merchantRepository.GetMerchantById(merchantStoreId);
            var paymentServices = merchant.PaymentServices.Select(ps => ps.PaymentService).ToList();
            var paymentOptions = _mapper.Map<List<PaymentServiceDto>>(paymentServices);

            return paymentOptions;
        }

        /// <summary>
        /// Gets payment options for orderId id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<List<PaymentServiceDto>> GetPaymentOptionsForOrder(Guid orderId)
        {
            Log.Information($"Getting payment options for order with payment request id: {orderId}");
            var order = await _paymentRequestRepository.GetRequestByPaymentRequestId(orderId);
            var merchant = await _merchantRepository.GetMerchantById(order.MerchantId);
            var paymentServices = merchant.PaymentServices.Select(ps => ps.PaymentService).ToList();
            var paymentOptions = _mapper.Map<List<PaymentServiceDto>>(paymentServices);

            return paymentOptions;
        }

        /// <summary>
        /// Saving payment request from consumer application
        /// </summary>
        /// <param name="redirectDto"></param>
        /// <returns></returns>
        public async Task<bool> SavePaymentRequest(RedirectDto redirectDto)
        {
            var paymentRequest = new Data.Entities.PaymentRequest
            {
                MerchantId = redirectDto.MerchantId,
                MerchantOrderId = redirectDto.OrderId,
                Amount = redirectDto.Amount,
                Id = redirectDto.OrderId,
                MerchantTimestamp = DateTime.UtcNow
            };

            var result = await _paymentRequestRepository.SaveRequest(paymentRequest);

            return result;
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