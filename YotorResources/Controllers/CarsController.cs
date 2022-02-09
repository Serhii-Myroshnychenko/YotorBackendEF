using Microsoft.AspNetCore.Authorization;
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
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IVerifyRepository _verifyRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public CarsController(ICarRepository carRepository, IVerifyRepository verifyRepository)
        {
            _carRepository = carRepository;
            _verifyRepository = verifyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCarsAsync()
        {
            try
            {
                return Ok(await _carRepository.GetCarsAsync());
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarAsync(int id)
        {
            try
            {
                var car = await _carRepository.GetCarAsync(id);
                if (car != null)
                {
                    return Ok(car);
                }
                return NotFound("Что-то пошло не так");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Popular")]
        public async Task<IActionResult> GetMostPopularCarsAsync()
        {
            try
            {
                return Ok(await _carRepository.GetMostPopularCarsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateCarAsync([FromForm] CarConstructor carConstructor)
        {
            try
            {
                var isLandlord = await _verifyRepository.IsLandlordAsync(UserId);
                if (isLandlord != null)
                {

                    await _carRepository.CreateCarAsync(isLandlord.OrganizationId, carConstructor.Model, carConstructor.Brand, carConstructor.Year, carConstructor.Transmission, carConstructor.Address, true, carConstructor.Type, carConstructor.Price, null, carConstructor.Description, carConstructor.Number);
                    return Ok("Успешно");

                }
                else
                {
                    return Unauthorized("Вы не являетесь арендодателем");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCarAsync(int id, [FromForm] CarConstructor carConstructor)
        {
            try
            {

                bool isAdmin = await _verifyRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {

                    await _carRepository.UpdateCarAsync(id, carConstructor.Model, carConstructor.Brand, carConstructor.Year, carConstructor.Transmission, carConstructor.Address, carConstructor.Status, carConstructor.Type, carConstructor.Price, null, carConstructor.Description, carConstructor.Number);
                    return Ok("Успешно");
                }
                return NotFound("Недостаточно прав");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
