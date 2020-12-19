using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Dtos;
using PaymentManager.Api.Repository.Interfaces;
using Serilog;

namespace PaymentManager.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IWebStoreRepository _webStoreRepository;

        public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, IMapper mapper, IWebStoreRepository webStoreRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _mapper = mapper;
            _webStoreRepository = webStoreRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto userForRegisterDto)
        {
            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(userToCreate, "User");
                if (roleResult.Succeeded)
                {
                    Log.Information("User registered.");
                    return Ok(userToCreate);
                }
                else
                {
                    Log.Error("User failed to register.");
                    return BadRequest(roleResult.Errors);
                }
            }
            else
            {
                Log.Error("User failed to register.");
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userForLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userForLoginDto.Email);

            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users
                    .Include(u => u.UserRoles).ThenInclude(r => r.Role)
                    .FirstOrDefaultAsync(u => u.NormalizedEmail == userForLoginDto.Email.ToUpper());

                Log.Information("User signed in into system.");
                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,
                    user = appUser
                });
            }
            else
            {
                Log.Error("User failed to login.");
                return Unauthorized();
            }
        }

        [HttpPost("requesttoken")]
        public async Task<IActionResult> RequestToken(WebStoreTokenRequestDto request)
        {
            Log.Information($"Client with id {request.WebStoreId} and {request.WebStoreName} requested token");
            var webStore = await _webStoreRepository.GetWebStoreById(request.WebStoreId);

            if (webStore == null)
            {
                Log.Information($"Client with id {request.WebStoreId} does not exist");
                return BadRequest($"Client with id {request.WebStoreId} does not exist");
            }
            else
            {
                Log.Information($"Client with id {request.WebStoreId} successful registered on system");
                return Ok(new
                {
                    token = GenerateJwtTokenForClient(webStore)
                });
            }
        }

        private async Task<String> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = "paymentmanager",
                Audience = "http://localhost:4200, http://localhost:3000"
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string GenerateJwtTokenForClient(WebStore webStore)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, webStore.Id.ToString()),
                new Claim(ClaimTypes.Name, webStore.StoreName),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = "paymentmanager",
                Audience = "http://localhost:4200, http://localhost:3000"
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
