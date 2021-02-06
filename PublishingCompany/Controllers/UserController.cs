using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using PublishingCompany.Api.Data.Entities;
using PublishingCompany.Api.Dtos;
using PublishingCompany.Api.Repository.Interfaces;

namespace PublishingCompany.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto userForLoginDto)
        {
            var user = await _unitOfWork.UserRepository.LogInUser(userForLoginDto.Email, userForLoginDto.Password);


            if (user != null)
            {
                Log.Information("User signed in into system.");
                return Ok(new
                {
                    user
                });
            }
            else
            {
                Log.Error("User failed to login.");
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("get-users-books")]
        public async Task<IActionResult> GetUserBoughtBooks(Guid userId)
        {
            var books = await _unitOfWork.UserRepository.GetUserBoughtBooks(userId);

            return Ok(books);
        }

        [HttpPost]
        [Route("subscribe-user")]
        public async Task<IActionResult> SubscribeUser(Guid userId)
        {
            await _unitOfWork.UserRepository.SetUserSubscription(userId);
            return Ok();
        }
    }
}
