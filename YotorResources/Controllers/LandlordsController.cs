using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;
using YotorResources.Contracts;
using YotorResources.Models;

namespace YotorResources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandlordsController : ControllerBase
    {
        private readonly ILandlordRepository _landlordRepository;
        private readonly IVerifyRepository _verifyRepository;


        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public LandlordsController(ILandlordRepository landlordRepository, IVerifyRepository verifyRepository)
        {
            _landlordRepository = landlordRepository;
            _verifyRepository = verifyRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetLandlordsAsync()
        {
            try
            {
                bool isAdmin = await _verifyRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    var landlords = await _landlordRepository.GetLandlordsAsync();
                    return Ok(landlords);
                }
                else
                {
                    return BadRequest("Вы не являетесь администратором");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetLandlordAsync(int id)
        {
            try
            {
                bool isAdmin = await _verifyRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    return Ok(await _landlordRepository.GetLandlordAsync(id));
                }
                else
                {
                    return BadRequest("Вы не являетесь администратором");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateLandlordAsync([FromForm] LandlordConstructor landlordConstructor)
        {
            try
            {
                bool isAdmin = await _verifyRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    var organizByName = await _verifyRepository.GetOrganizationByNameAsync(landlordConstructor.OrganizationName);
                    var customerByName = await _verifyRepository.GetCustomerByNameAsync(landlordConstructor.CustomerName);
                    if (organizByName != null && customerByName != null)
                    {
                        var isUserMemberOfTheOrganization = await _verifyRepository.IsLandlordAsync(customerByName.UserId);
                        bool isUser = await _verifyRepository.IsUserAsync(customerByName.UserId);
                        bool isOrganization = await _verifyRepository.IsOrganizationAsync(organizByName.OrganizationId);

                        if (isUser == true && isOrganization == true && isUserMemberOfTheOrganization == null)
                        {
                            await _landlordRepository.CreateLandlordAsync(customerByName.UserId, organizByName.OrganizationId, customerByName.FullName);
                            return Ok("Ok");
                        }
                        else
                        {
                            return NotFound("Данные не являются корректными");
                        }
                    }
                    else
                    {
                        return BadRequest("Что-то пошло не так");
                    }
                }
                else
                {
                    return BadRequest("Вы не являетесь администратором");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateLandlordAsync(int id, Landlord landlord)
        {
            try
            {
                bool isAdmin = await _verifyRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    bool isUser = await _verifyRepository.IsUserAsync(landlord.UserId);
                    bool isOrganization = await _verifyRepository.IsOrganizationAsync(landlord.OrganizationId);

                    if (isUser == true && isOrganization == true)
                    {
                        await _landlordRepository.UpdateLandlordAsync(id, landlord);
                        return Ok("Ok");
                    }
                    else
                    {
                        return NotFound("Данные не являются корректными");
                    }

                }
                else
                {
                    return BadRequest("Вы не являетесь администратором");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
