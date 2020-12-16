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

        public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _mapper = mapper;
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
                Audience = "http://localhost:4200"
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
