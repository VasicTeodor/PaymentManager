using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Dtos;
using PaymentManager.Api.Services.Interfaces;
using Serilog;

namespace PaymentManager.Api.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IGenericRestClient _restClient;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentController(IGenericRestClient restClient, IPaymentService paymentService, IMapper mapper)
        {
            _restClient = restClient;
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("paybypaymentcard")]
        public async Task<IActionResult> PayByPaymentBank(PaymentRequestDto paymentRequestDto)
        {
            Log.Information("Received payment request for payment by card");
            var paymentRequest =
                await _paymentService.GeneratePaymentRequest(paymentRequestDto.MerchantStoreId, paymentRequestDto.Amount);

            Log.Information($"SENDING PAYMENT REQUEST: {paymentRequest.ToString()}");
            var result = await _restClient.PostRequest<PaymentRequestResponseDto>(paymentRequestDto.PaymentServiceUrl, paymentRequest);

            if (result == null || result.PaymentId == Guid.Empty || string.IsNullOrEmpty(result.PaymentUrl))
            {
                Log.Information($"PAYMENT REQUEST FAILED: {paymentRequest.ToString()}");
                return BadRequest("Payment request failed");
            }

            Log.Information($"PAYMENT REQUEST SUCCEEDED: {paymentRequest.ToString()}");
            Log.Information("Sending url for redirection to client");
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("finishpayment")]
        public async Task<IActionResult> FinishPayment(TransactionResultDto transactionResultDto)
        {
            Log.Information($"Received request for finishing transaction with status: {transactionResultDto.Status}");
            var transactionResult = _mapper.Map<Transaction>(transactionResultDto);

            var result = await _paymentService.CompleteTransaction(transactionResult);

            Log.Information($"TRANSACTION FINISHED: {transactionResultDto}");
            Log.Information($"Redirecting user: {result.UrlForRedirection}");
            return RedirectPermanent(result.UrlForRedirection);
        }

        [HttpGet]
        [Route("getpaymentoptions")]
        public async Task<IActionResult> GetPaymentOptions(string merchantUniqueStoreId)
        {
            Log.Information($"Request for payment options for merchant: {merchantUniqueStoreId}");
            var options = await _paymentService.GetPaymentOptions(merchantUniqueStoreId);
            return Ok(options);
        }
    }
}
