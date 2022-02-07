using BLL.BL;
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
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IVerifyRepository _verifyRepository;
        private readonly BookingСoefficient _bookingСoefficient;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public BookingsController(IBookingRepository bookingRepository,IVerifyRepository verifyRepository)
        {
            _bookingRepository = bookingRepository;
            _verifyRepository = verifyRepository;
            _bookingСoefficient = new BookingСoefficient();
        }

        [HttpGet]
        public async Task<IActionResult> GetBookingsAsync()
        {
            try
            {
                bool isAdmin = await _verifyRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    return Ok(await _bookingRepository.GetBookingsAsync());
                }
                else
                {
                    return Unauthorized("Вы не являетесь администратором");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingAsync(int id)
        {
            try
            {
                bool isAdmin = await _verifyRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    var booking = await _bookingRepository.GetBookingAsync(id);
                    if (booking != null)
                    {
                        return Ok(booking);
                    }
                    else
                    {
                        return BadRequest("Что-то пошло не так");
                    }

                }
                else
                {
                    return Unauthorized("Вы не являетесь администратором");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("User")]
        public async Task<IActionResult> GetBookingsByUserIdAsync()
        {
            try
            {
                return Ok(await _bookingRepository.GetBookingsByUserIdAsync(UserId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateBookingAsync(BookingConstructor bookingConstructor)
        {
            try
            {
                var restriction = await _verifyRepository.GetRestrictionByCarNameAsync(bookingConstructor.CarName);
                var car = await _verifyRepository.GetCarByCarNameAsync(bookingConstructor.CarName);
                int countOfDays = bookingConstructor.EndDate.Day - bookingConstructor.StartDate.Day;
                double coefficient = _bookingСoefficient.CalculateСoefficient(countOfDays);
                int totalPrice = (int)(bookingConstructor.FullPrice * countOfDays * coefficient);

                if (car != null && restriction != null)
                {
                    await _bookingRepository.CreateBookingAsync(restriction.RestrictionId, UserId, car.CarId, null, bookingConstructor.StartDate, bookingConstructor.EndDate, false, totalPrice, bookingConstructor.StartAddress, bookingConstructor.EndAddress);
                    await _verifyRepository.UpdateStatusCarAsync(car.CarId);
                    return Ok("Ok");
                }
                else if (car != null && restriction == null)
                {
                    await _bookingRepository.CreateBookingAsync(null, UserId, car.CarId, null, bookingConstructor.StartDate, bookingConstructor.EndDate, false, totalPrice, bookingConstructor.StartAddress, bookingConstructor.EndAddress);
                    await _verifyRepository.UpdateStatusCarAsync(car.CarId);
                    return Ok("Ok");
                }
                else
                {
                    return NotFound("Что-то пошло нет так");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
