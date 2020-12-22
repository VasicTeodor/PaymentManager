using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Dtos;
using PaymentManager.Api.Repository.Interfaces;
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
        private readonly IConfiguration _config;
        private readonly IWebStoreRepository _webStoreRepository;
        private readonly IMapper _mapper;

        public PaymentController(IGenericRestClient restClient, IPaymentService paymentService, IMapper mapper, IConfiguration config, IWebStoreRepository webStoreRepository)
        {
            _restClient = restClient;
            _paymentService = paymentService;
            _mapper = mapper;
            _config = config;
            _webStoreRepository = webStoreRepository;
        }

        [HttpPost]
        [Route("paybypaymentcard")]
        public async Task<IActionResult> PayByPaymentBank(PaymentRequestDto paymentRequestDto)
        {
            Log.Information("Received payment request for payment by card");
            var paymentRequest =
                await _paymentService.GeneratePaymentRequest(paymentRequestDto.Amount, paymentRequestDto.OrderId);

            Log.Information($"SENDING PAYMENT REQUEST: {paymentRequest.ToString()}");
            var result = await _restClient.PostRequest<PaymentRequestResponseDto>(paymentRequestDto.PaymentServiceUrl+ "payment/CheckPaymentRequest", paymentRequest);

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
            return Ok(new
            {
                redirectUrl = result.UrlForRedirection
            });
        }

        [HttpGet]
        [Route("getpaymentoptions")]
        public async Task<IActionResult> GetPaymentOptions(Guid merchantId)
        {
            Log.Information($"Request for payment options for merchant: {merchantId}");
            var options = await _paymentService.GetPaymentOptions(merchantId);
            return Ok(options);
        }

        [HttpGet]
        [Route("getpaymentrequestdetails")]
        public async Task<IActionResult> GetPaymentRequestDetails(Guid orderId)
        {
            Log.Information($"Request for payment request details for orderId: {orderId}");
            var details = await _paymentService.GetPaymentRequestDetails(orderId);

            if (details != null)
            {
                return Ok(details);
            }

            return BadRequest("Required payment does not exist");
        }

        [HttpGet]
        [Route("getpaymentoptionsfororder")]
        public async Task<IActionResult> GetPaymentOptionsForOrder(Guid orderId)
        {
            Log.Information($"Request for payment options for payment request with id: {orderId}");
            var options = await _paymentService.GetPaymentOptionsForOrder(orderId);
            return Ok(options);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("redirecttopaymentmanagerapp")]
        public async Task<IActionResult> RedirectToPaymentManagerApp(RedirectDto redirectDto)
        {
            Log.Information($"Application {redirectDto.StoreName} requesting redirect to payment manager app");
            var store = await _webStoreRepository.GetWebStoreByIdAndName(redirectDto.StoreId, redirectDto.StoreName);
            if (store == null)
            {
                return BadRequest("There is no store with received credentials");
            }

            var token = GenerateJwtTokenForClient(store);
            if (redirectDto.OrderId == Guid.Empty)
            {
                redirectDto.OrderId = Guid.NewGuid();
            }
            
            var result = await _paymentService.SavePaymentRequest(redirectDto);
            if (!result)
            {
                Log.Error($"There was error saving request for application: {redirectDto.StoreName}");
                return BadRequest("There was error saving request");
            }
            
            var url = $"http://localhost:4200/paymentoptions?token={token}&orderId={redirectDto.OrderId}";
            Log.Information($"Token created for application {redirectDto.StoreName}");
            // return RedirectPermanent(url);
            return Ok(new
            {
                redirectUrl = url
            });
        }

        private string GenerateJwtTokenForClient(WebStore webStore)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, webStore.Id.ToString()),
                new Claim(ClaimTypes.Name, webStore.StoreName),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = creds,
                Issuer = "paymentmanager",
                Audience = "http://localhost:4200, http://localhost:3000"
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
