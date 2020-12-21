using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayPal.Service.Dtos;
using PayPal.Service.Services.Interfaces;
using Serilog;

namespace PayPal.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private readonly IPayPalService _payPalService;

        public PayPalController(IPayPalService payPalService)
        {
            _payPalService = payPalService;
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
        public async Task<IActionResult> Success(string paymentId, string token, string payerId)
        {
            Log.Information($"Payment with id {paymentId}, by payer {payerId} successfully finished");
            return Ok("Success");
        }


        [HttpGet]
        [Route("cancel")]
        public async Task<IActionResult> Cancel(string token)
        {
            Log.Information($"Payment with token {token}, canceled");
            return Ok("Canceled");
        }

        [HttpPost]
        [Route("executepayment")]
        public async Task<IActionResult> ExecutePayment(PaymentExecuteDto paymentExecuteDto)
        {
            Log.Information($"Payment with id {paymentExecuteDto.PaymentId} by payer with id {paymentExecuteDto.PayerId} executed");
            var result = await _payPalService.ExecutePayment(paymentExecuteDto.PaymentId, paymentExecuteDto.PayerId, paymentExecuteDto.Token);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("There was an error");
        }
    }
}
