using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PayPal.Service.Dtos;
using PayPal.Service.Repository.Interfaces;
using PayPal.Service.Services.Interfaces;
using Serilog;

namespace PayPal.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private readonly IPayPalService _payPalService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IGenericRestClient _restClient;
        private readonly IPaymentRequestRepository _repository;
        private readonly IConfiguration _configuration;

        public PayPalController(IPayPalService payPalService, IGenericRestClient restClient, IPaymentRequestRepository repository, IConfiguration configuration, ISubscriptionService subscriptionService)
        {
            _payPalService = payPalService;
            _restClient = restClient;
            _repository = repository;
            _configuration = configuration;
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        [Route("createpayment")]
        public async Task<IActionResult> CreatePayment(PaymentRequestDto paymentRequestDto)
        {
            Log.Information("Received request for payment with pay pal");
            var result = await _payPalService.CreatePayment(paymentRequestDto);

            if (result != null)
            {

                Log.Information("Payment created, sending payment links back to client");
                return Ok(new
                {
                    id = result.id,
                    links = result.links.Select(link => new {type = link.rel, url = link.href}).ToList()
                }); 
            }

            Log.Information("Payment creation declined, there was error while creating payment");
            return BadRequest("Your PayPal account is not verificated!");
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success(string paymentId, string token, string payerId)
        {
            Log.Information($"Payment with id {paymentId}, by payer {payerId} successfully finished");
            return Ok("Success");
        }


        [HttpGet]
        [Route("cancel")]
        public  IActionResult Cancel(string token)
        {
            Log.Information($"Payment with token {token}, canceled");
            return Ok("Canceled");
        }

        [HttpPost]
        [Route("executepayment")]
        public async Task<IActionResult> ExecutePayment(PaymentExecuteDto paymentExecuteDto)
        {
            Log.Information($"Payment with id {paymentExecuteDto.PaymentId} by payer with id {paymentExecuteDto.PayerId} executed");
            var paymentManagerUrl = _configuration.GetSection("PaymentManagerApi:BaseUrl").Value;
            var actionUrl = _configuration.GetSection("PaymentManagerApi:FinishTransaction").Value;
            var result = await _payPalService.ExecutePayment(paymentExecuteDto.PaymentId, paymentExecuteDto.PayerId, paymentExecuteDto.Token);
            if (result != null)
            {
                var request = await _repository.GetPaymentRequest(paymentExecuteDto.PaymentId);

                var transaction = new TransactionDto()
                {
                    Amount = request.Amount,
                    PaymentId = request.Id,
                    MerchantOrderId = request.OrderId,
                    Status = "SUCCESS",
                    AcquirerOrderId = Guid.NewGuid(),
                    AcquirerTimestamp = DateTime.UtcNow
                };

                var paymentManagerResponse = await _restClient.PostRequest<RedirectDto>($"{paymentManagerUrl}{actionUrl}", transaction);
                Log.Information("Sending transaction result to payment manager");
                return Ok(paymentManagerResponse);
            }
            else
            {
                var request = await _repository.GetPaymentRequest(paymentExecuteDto.PaymentId);

                var transaction = new TransactionDto()
                {
                    Amount = request.Amount,
                    PaymentId = request.Id,
                    MerchantOrderId = request.OrderId,
                    Status = "FAILED",
                    AcquirerOrderId = Guid.NewGuid(),
                    AcquirerTimestamp = DateTime.UtcNow
                };

                var paymentManagerResponse = await _restClient.PostRequest<RedirectDto>($"{paymentManagerUrl}{actionUrl}", transaction);
                Log.Information("Sending transaction result to payment manager");
                return Ok(paymentManagerResponse);
            }
        }

        [HttpPost]
        [Route("billingplan/create")]
        public async Task<IActionResult> CreateBillingPlan(BillingPlanRequestDto billingPlanRequest)
        {
            var plan = await _subscriptionService.CreateBillingPlan(billingPlanRequest);
            return Ok(plan);
        }

        [HttpPost]
        [Route("subscription/create")]
        public async Task<IActionResult> CreateSubscription(SubscriptionRequestDto subscriptionRequest)
        {
            var result = await _subscriptionService.CreateSubscription(subscriptionRequest);
            return Ok(result);
        }

        [HttpPost]
        [Route("subscription/success")]
        public async Task<IActionResult> SubscriptionSuccesful(string token)
        {
            Log.Information($"Success subscription with token '{token}'");
            return Ok("Success!");
        }

        [HttpPost]
        [Route("subscription/cancel")]
        public async Task<IActionResult> SubscriptionCanceled(string token)
        {
            Log.Information($"Canceled subscription for token {token}");
            return Ok("Canceled!");
        }

        [HttpGet]
        [Route("subscription/get")]
        public async Task<IActionResult> GetSubscriptions(Guid webStoreId)
        {
            var getSubscriptions = new GetSubscriptionsDto()
            {
                WebStoreId = webStoreId
            };
            var data = await _subscriptionService.GetSubscriptions(getSubscriptions);
            return Ok(data);
        }

        [HttpPost]
        [Route("subscription/execute")]
        public async Task<IActionResult> ExecuteSubscription(ExecuteSubscriptionDto executeSubscription)
        {
            var result = await _subscriptionService.ExecuteSubscription(executeSubscription);
            return Ok(result);
        }
    }
}
