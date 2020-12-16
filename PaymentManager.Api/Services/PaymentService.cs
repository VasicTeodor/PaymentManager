using System;
using System.Threading.Tasks;
using PaymentManager.Api.Models;
using PaymentManager.Api.Repository.Interfaces;
using PaymentManager.Api.Services.Interfaces;

namespace PaymentManager.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMerchantRepository _merchantRepository;

        public PaymentService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        /// <summary>
        /// Creates payment request for merchants bank
        /// </summary>
        /// <param name="merchantId">unique id for merchant</param>
        /// <param name="amount">money amount for transaction</param>
        /// <returns></returns>
        public async Task<PaymentRequest> GeneratePaymentRequest(string merchantId, decimal amount)
        {
            var merchant = await _merchantRepository.GetMerchant(merchantId);

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

            return paymentRequest;
        }
    }
}