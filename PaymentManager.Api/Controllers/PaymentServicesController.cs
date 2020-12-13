using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Repository.Interfaces;
using Serilog;

namespace PaymentManager.Api.Controllers
{
    [Authorize(Roles = "Admin")]
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
        [Route("getpaymentservices")]
        public async Task<IActionResult> GetPaymentServices(int pageNumber = 1, int pageSize = 10)
        {
            Log.Information("Request to get all payment services");
            var services = await _repository.GetPaymentServices(pageNumber, pageSize);
            return Ok(services);
        }

        [HttpGet]
        [Route("getpaymentservice")]
        public async Task<IActionResult> GetPaymentService(Guid id)
        {
            Log.Information($"Request to get payment services with id {id}");
            var service = await _repository.GetPaymentServiceById(id);
            return Ok(service);
        }

        [HttpPost]
        [Route("addpaymentservice")]
        public async Task<IActionResult> AddPaymentService(PaymentService newService)
        {
            Log.Information("Request to add new payment service");
            var result = await _repository.AddPaymentService(newService);
            
            if (result)
                return Ok(result);
            
            return BadRequest();
        }

        [HttpDelete]
        [Route("removepaymentservice")]
        public async Task<IActionResult> RemovePaymentService(Guid id)
        {
            Log.Information($"Request to remove payment service with id {id}");
            var result = await _repository.RemovePaymentService(id);

            if (result)
                return Ok(result);

            return BadRequest();
        }
    }
}
