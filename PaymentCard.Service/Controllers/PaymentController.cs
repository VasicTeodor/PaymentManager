using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Service.Dto;
using Bank.Service.Models;
using Bank.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Bank.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IGenericRestClient _restClient;
        private readonly IIssuerPaymentService _issuerPaymentService;
        private string paymentManagerApiUrl = "https://localhost:5021/api/payment/";

        public PaymentController(IPaymentService paymentService, IGenericRestClient restClient, IIssuerPaymentService issuerPaymentService)
        {
            _restClient = restClient;
            _paymentService = paymentService;
            _issuerPaymentService = issuerPaymentService;
        }

        // GET api/values  
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var port = Request.Host.Port;

            return Ok(String.Join(", ", new string[] { "Payment via PaymentCard", "PaymentCard.Service", port.Value.ToString() }));
        }

        [HttpGet]
        [Route("GetById")]
        public ActionResult<IEnumerable<string>> GetById(string id)
        {
            var port = Request.Host.Port;

            return Ok(String.Join(", ", new string[] { "Payment via PaymentCard", "PaymentCard.Service", $"ID received {id}", port.Value.ToString() }));
        }

        [HttpPost]
        [Route("CheckPaymentRequest")]
        public ActionResult<PaymentRequestResponseDto> CheckPayment([FromBody] PaymentRequest paymentRequest)
        {
            Log.Information($"Bank service receieved payment request {paymentRequest.ToString()}");
            var responseDto = _paymentService.ValidatePayment(paymentRequest);
            return Ok(responseDto);
        }

        [HttpPost]
        [Route("FrontPayment")]
        public async Task<ActionResult<TransactionDto>> SubmitPayment(Guid Id,[FromBody] CardDto cardDto)
        {
            Log.Information($"Bank service reveived user payment request from bank");
            var transportDto = _paymentService.SubmitPayment(cardDto, Id);
            Log.Information($"Bank service sending Transaction data to PaymentManager to finish transaction");
            var callPaymentManager = await _restClient.PostRequest<RedirectDto>($"{paymentManagerApiUrl}finishpayment", transportDto);
            return Ok(callPaymentManager);
        }

        [HttpPost]
        [Route("IssuerPayment")]
        public ActionResult<ResponseDto> IssuerPayment(RequestDto issuerRequest)
        {
            ResponseDto response = _issuerPaymentService.IssuerPayment(issuerRequest);
            return Ok(response);
        }
    }
}