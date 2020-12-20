using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayPal.Service.Dtos;
using PayPal.Service.Services.Interfaces;

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
            var result = await _payPalService.CreatePayment(paymentRequestDto);

            if (result != null)
            {
                foreach (var link in result.links)
                {
                    if (link.rel.Equals("approval_url"))
                    {
                        return Ok(new { address = link.href });
                    }
                }
            }

            return BadRequest("Your PayPal account is not verificated!");
        }

        [HttpGet]
        [Route("success")]
        public async Task<IActionResult> ExecutePayment(string paymentId, string token, string payerId, string email = null)
        {
            var result = await _payPalService.ExecutePayment(paymentId, payerId, email);

            return Redirect("http://localhost:4200/tickets;success=1");
        }

        [HttpGet]
        [Route("cancel")]
        public async Task<IActionResult> CancelPayment()
        {
            return Redirect("http://localhost:4200/tickets;success=0");
        }
    }
}
