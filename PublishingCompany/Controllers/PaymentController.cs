using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PublishingCompany.Api.Dtos;
using PublishingCompany.Api.Repository.Interfaces;
using PublishingCompany.Api.Services.Interfaces;
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
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentController(IGenericRestClient restClient, IConfiguration configuration, IUnitOfWork unitOfWork, IOrderService orderService)
        {
            _restClient = restClient;
            _configuration = configuration;
            _paymentManagerUrl = _configuration.GetSection("PaymentManagerUrl").Value;
            _unitOfWork = unitOfWork;
            _orderService = orderService;
        }

        [HttpPost]
        [Route("create-order")]
        public async Task<IActionResult> CrateOrder(OrderDto orderDto)
        {
            var orderId = await _orderService.CreateOrder(orderDto);
            if (orderId != Guid.Empty && orderId != null)
            {
                return Ok(new {orderId});
            }

            return BadRequest("Error while creating new order");
        }

        [HttpPost]
        [Route("complete-order")]
        public async Task<IActionResult> CompleteOrder(CompleteOrderDto completeOrderDto)
        {
            var result = await _orderService.CompleteOrder(completeOrderDto.OrderId, completeOrderDto.OrderStatus);

            return Ok(result);
        }
    }
}
