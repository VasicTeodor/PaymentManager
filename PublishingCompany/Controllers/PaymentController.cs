using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PublishingCompany.Infrastructure.Interface;

namespace PublishingCompany.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IGenericRestClient _restClient;
        private readonly IConfiguration _configuration;
        private readonly string _paymentManagerUrl;

        public PaymentController(IGenericRestClient restClient, IConfiguration configuration)
        {
            _restClient = restClient;
            _configuration = configuration;
            _paymentManagerUrl = _configuration.GetSection("PaymentManagerUrl").Value;
        }

        [HttpGet]
        [Route("GetPayPal")]
        public async Task<IActionResult> GetPayPal()
        {
            var result = await _restClient.Get<String>($"{_paymentManagerUrl}paypal/paypal");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetPayPalValue")]
        public async Task<IActionResult> GetPayPalValue(string value)
        {
            var result = await _restClient.Get<String>($"{_paymentManagerUrl}paypal/paypal/getbyid?id={value}");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetBitcoin")]
        public async Task<IActionResult> GetBitcoin()
        {
            var result = await _restClient.Get<String>($"{_paymentManagerUrl}bitcoin/bitcoin");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetBitcoinValue")]
        public async Task<IActionResult> GetBitcoinValue(string value)
        {
            var result = await _restClient.Get<String>($"{_paymentManagerUrl}bitcoin/bitcoin/getbyid?id={value}");
            return Ok(result);
        }

    }
}
