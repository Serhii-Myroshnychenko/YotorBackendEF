using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YotorBackendEF.Contracts;
using YotorBackendEF.Models;
using YotorContext.Helpers;
using YotorContext.Models;

namespace YotorBackendEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IOptions<AuthOptions> _options;
        private readonly ICustomerRepository _customerRepository;
        public CustomersController(ICustomerRepository customerRepository, IOptions<AuthOptions> options)
        {
            _customerRepository = customerRepository;
            _options = options;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            try
            {
                var customers = await _customerRepository.GetCustomersAsync();
                return Ok(customers);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }   
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> RegistrationAsync([FromBody]Registration registration)
        {
            try
            {
                await _customerRepository.RegistrationAsync(registration.Full_name, registration.Email, registration.Phone, registration.Password, false);
                return Ok("Ok");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]Login login)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerAsync(login.Email, login.Password);
                if (customer != null)
                {
                    var token = GenerateJWT(customer);
                    return Ok(new
                    {
                        access_token = token
                    });
                }
                return NotFound("Неверный логин или пароль");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        

        private string GenerateJWT(Customer customer)
        {
            var authParams = _options.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim("user_id", customer.UserId.ToString()),
                new Claim(ClaimTypes.Name, customer.FullName),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim(ClaimTypes.MobilePhone, customer.Phone),
                new Claim("is_admin", customer.IsAdmin.ToString())

            };
            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
