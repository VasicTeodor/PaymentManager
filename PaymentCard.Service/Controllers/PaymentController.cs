using System;
using System.Collections.Generic;
using Bank.Service.Dto;
using Bank.Service.Models;
using Bank.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
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
            var responseDto = _paymentService.ValidatePayment(paymentRequest);
            return Ok(responseDto);
        }

        [HttpPost]
        [Route("FrontPayment")]
        public ActionResult SubmitPayment([FromBody] CardDto cardDto, string orderId)
        {

            return Ok();
        }
    }
}