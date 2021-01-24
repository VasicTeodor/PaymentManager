using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Dtos;
using PaymentManager.Api.Repository.Interfaces;
using Serilog;

namespace PaymentManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantRepository _repository;
        private readonly IMapper _mapper;

        public MerchantController(IMerchantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getmerchants")]
        public async Task<IActionResult> GetMerchants(int pageNumber = 1, int pageSize = 10)
        {
            Log.Information("Request to get all merchants");
            var services = await _repository.GetMerchants(pageNumber, pageSize);
            return Ok(services);
        }

        [HttpGet]
        [Route("getmerchant")]
        public async Task<IActionResult> GetMerchant(Guid id)
        {
            Log.Information($"Request to get merchant with id {id}");
            var service = await _repository.GetMerchantById(id);
            return Ok(service);
        }

        [HttpPost]
        [Route("addmerchant")]
        public async Task<IActionResult> AddPaymentService(AddMerchantDto newMerchantDto)
        {
            Log.Information("Request to add new merchant");
            var newMerchant = _mapper.Map<Merchant>(newMerchantDto);
            newMerchant.WebStore = new WebStore()
            {
                Id = newMerchantDto.WebStoreId
            };
            newMerchant.PaymentServices = new List<MerchantPaymentServices>();
            foreach (var service in newMerchantDto.PaymentServices)
            {
                newMerchant.PaymentServices.Add(new MerchantPaymentServices
                {
                    PaymentServiceId = service,
                    Merchant = newMerchant
                });
            }
            var result = await _repository.AddMerchant(newMerchant);

            if (result)
                return Ok(result);

            return BadRequest();
        }

        [HttpPut]
        [Route("editmerchant")]
        public async Task<IActionResult> EditPaymentService([FromQuery] Guid id, Merchant merchant)
        {
            Log.Information("Request to edit existing merchant");
            var result = await _repository.UpdateMerchant(id, merchant);

            if (result)
                return Ok(result);

            return BadRequest();
        }

        [HttpDelete]
        [Route("removemerchant")]
        public async Task<IActionResult> RemovePaymentService(Guid id)
        {
            Log.Information($"Request to remove merchant with id {id}");
            var result = await _repository.RemoveMerchant(id);

            if (result)
                return Ok(result);

            return BadRequest();
        }
    }
}
