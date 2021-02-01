using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitCoin.Service.Dtos;
using BitCoin.Service.Models;
using BitCoin.Service.Repository.Interfaces;
using BitCoin.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BitCoin.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BitCoinController : ControllerBase
    {
        private readonly ICoingateService _coingateService;
        private readonly IOrderRepository _repository;
        private readonly IGenericRestClient _restClient;
        private readonly IConfiguration _configuration;

        public BitCoinController(ICoingateService coingateService, IOrderRepository repository, IGenericRestClient restClient, IConfiguration configuration)
        {
            _coingateService = coingateService;
            _repository = repository;
            _restClient = restClient;
            _configuration = configuration;
        }

        // GET api/values  
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var port = Request.Host.Port;

            return Ok(String.Join(", ", new string[] { "Payment via Bitcoin", "Bitcoin.Service", port.Value.ToString() }));
        }

        [HttpGet]
        [Route("GetById")]
        public ActionResult<IEnumerable<string>> GetById(string id)
        {
            var port = Request.Host.Port;

            return Ok(String.Join(", ", new string[] { "Payment via Bitcoin", "Bitcoin.Service", $"ID received {id}", port.Value.ToString() }));
        }

        [HttpPost]
        [Route("create-payment")]
        public async Task<IActionResult> CreatePayment(OrderDto order)
        {
            Log.Information("Received request for payment with bitcoin");
            var orderResult = await _coingateService.CreatePayment(order);

            if (orderResult != null)
            {

                Log.Information("Payment created, sending payment links back to client");
                return Ok(orderResult);
            }

            Log.Information("Payment creation declined, there was error while creating payment");
            return BadRequest("Your Coingate account is not verificated!");

            
        }

        [HttpGet]
        [Route("payment-status")]
        public async Task<IActionResult> PaymentStatus(string orderId, string status)
        {
            TransactionDto transaction;
            Log.Information("Received response from coingate api");
            var paymentManagerUrl = _configuration.GetSection("PaymentManagerApi:BaseUrl").Value;
            var actionUrl = _configuration.GetSection("PaymentManagerApi:FinishTransaction").Value;
            var order = await _repository.GetOrder(orderId);

            transaction = new TransactionDto()
            {
                Amount = Convert.ToDecimal(order.PriceAmount),
                PaymentId = order.Id,
                MerchantOrderId = order.MerchantId,
                Status = "ERROR",
                AcquirerOrderId = Guid.NewGuid(),
                AcquirerTimestamp = DateTime.UtcNow
            };


            transaction.Status = status=="success" ? "SUCCESS" : "FAILED";

            var paymentManagerResponse = await _restClient.PostRequest<RedirectDto>($"{paymentManagerUrl}{actionUrl}", transaction);
            Log.Information("Redirecting to merchant store.");

            return Redirect(paymentManagerResponse.RedirectUrl);
        }
    }
}
