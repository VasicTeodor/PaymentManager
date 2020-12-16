using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class WebStoreController : ControllerBase
    {
        private readonly IWebStoreRepository _repository;

        public WebStoreController(IWebStoreRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getwebstore")]
        public async Task<IActionResult> GetWebStore([FromQuery]Guid id)
        {
            Log.Information($"Request for fetching web store with id: {id}");
            var webStore = await _repository.GetWebStoreById(id);

            return Ok(webStore);
        }

        [HttpGet]
        [Route("getwebstores")]
        public async Task<IActionResult> GetWebStores()
        {
            Log.Information($"Request for fetching web stores");
            var webStores = await _repository.GetWebStores();

            return Ok(webStores);
        }

        [HttpPost]
        [Route("addnewwebstore")]
        public async Task<IActionResult> AddNewWebStore(WebStore webStore)
        {
            Log.Information("Adding new web store to system");
            var result = await _repository.AddWebStore(webStore);

            if (result)
                return Ok(result);

            return BadRequest();
        }

        [HttpDelete]
        [Route("removewebstore")]
        public async Task<IActionResult> RemoveWebStore([FromQuery]Guid id)
        {
            Log.Information($"Remove web store with id {id}");
            var result = await _repository.RemoveWebStore(id);

            if (result)
                return Ok(result);

            return BadRequest();
        }

        [HttpPut]
        [Route("updatewebstore")]
        public async Task<IActionResult> UpdateWebStore([FromQuery]Guid id, WebStore webStore)
        {
            Log.Information($"Updating web store with id {id} into system");
            var result = await _repository.UpdateWebStore(id, webStore);

            if (result)
                return Ok(result);

            return BadRequest();
        }
    }
}
