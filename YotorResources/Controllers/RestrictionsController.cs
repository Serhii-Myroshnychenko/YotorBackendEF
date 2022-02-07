using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorResources.Contracts;
using YotorResources.Models;

namespace YotorResources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestrictionsController : ControllerBase
    {
        private readonly IRestrictionRepository _restrictionRepository;
        private readonly IVerifyRepository _verifyRepository;
        public RestrictionsController(IRestrictionRepository restrictionRepository, IVerifyRepository verifyRepository)
        {
            _restrictionRepository = restrictionRepository;
            _verifyRepository = verifyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetRestrictionsAsync()
        {
            try
            {
                return Ok(await _restrictionRepository.GetRestrictionsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Person")]
        public async Task<IActionResult> GetRestictionsOfSomePerson([FromHeader] int id)
        {
            try
            {
                return Ok(await _restrictionRepository.GetRestrictionsOfSomePersonAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRestrictionAsync([FromHeader] int Id)
        {
            try
            {
                await _restrictionRepository.DeleteRestrictionAsync(Id);
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateRestrictionAsync([FromBody] RestrictionConstructor restrictionConstructor)
        {
            try
            {
                var landlord = await _verifyRepository.IsLandlordAsync(int.Parse(restrictionConstructor.UserId));
                var isHisOrgan = await _verifyRepository.IsThisCarOfHisOrganizationAsync(restrictionConstructor.Name);
                if (landlord != null && isHisOrgan != null && landlord.OrganizationId == isHisOrgan.OrganizationId)
                {
                    await _restrictionRepository.CreateRestrictionAsync(landlord.LandlordId, restrictionConstructor.Name, restrictionConstructor.Description);
                    return Ok("Ok");
                }
                else
                {
                    return BadRequest("У вас нету доступа к данному автомобилю");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
