using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentManager.Api.Repository.Interfaces;
using Serilog;

namespace PaymentManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentServicesController : ControllerBase
    {
        private readonly IPaymentServiceRepository _repository;

        public PaymentServicesController(IPaymentServiceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getpaymentservicess")]
        public async Task<IActionResult> GetPaymentServices(int pageNumber = 1, int pageSize = 10)
        {
            Log.Information("Request to get all payment services");
            var services = await _repository.GetPaymentServices(pageNumber, pageSize);
            return Ok(services);
        }
    }
}
