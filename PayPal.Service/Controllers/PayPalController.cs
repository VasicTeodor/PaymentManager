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

        // GET api/values  
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var port = Request.Host.Port;

            return Ok(String.Join(", ",new string[] { "Payment via PayPal", "PayPal.Service", port.Value.ToString() }));
        }

        [HttpGet]
        [Route("GetById")]
        public ActionResult<IEnumerable<string>> GetById(string id)
        {
            var port = Request.Host.Port;

            return Ok(String.Join(", ",new string[] { "Payment via PayPal", "PayPal.Service", $"ID received {id}",port.Value.ToString() }));
        }

        [HttpPost]
        [Route("createpayment")]
        public async Task<IActionResult> CreatePayment(PaymentRequestDto paymentRequestDto)
        {
            var result = await _payPalService.CreatePayment(paymentRequestDto);
            return Ok(result);
        }
    }
}
