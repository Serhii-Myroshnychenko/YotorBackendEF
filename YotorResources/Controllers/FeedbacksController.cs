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
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IVerifyRepository _verifyRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public FeedbacksController(IFeedbackRepository feedbackRepository, IVerifyRepository verifyRepository)
        {
            _feedbackRepository = feedbackRepository;
            _verifyRepository = verifyRepository;
        }

        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetFeedbacksAsync()
        {
            try
            {
                bool isAdmin = await _verifyRepository.IsAdminAsync(UserId);
                if(isAdmin == true)
                {
                    return Ok(await _feedbackRepository.GetFeedbacksAsync());
                }
                else
                {
                    return BadRequest("Что-то пошло не так");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetFeedbackAsync(int id)
        {
            try
            {
                bool isAdmin = await _verifyRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    return Ok(await _feedbackRepository.GetFeedbackAsync(id));
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
        public async Task<IActionResult> CreateFeedbackAsync([FromBody] FeedbackConstructor feedbackConstructor)
        {
            try
            {
                DateTime time = DateTime.Today;
                await _feedbackRepository.CreateFeedbackAsync(UserId, feedbackConstructor.Name, time, feedbackConstructor.Text);
                return Ok("OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbackAsync(int id)
        {
            try
            {
                bool isAdmin = await _verifyRepository.IsAdminAsync(UserId);
                if (isAdmin == true)
                {
                    await _feedbackRepository.DeleteFeedbackAsync(id);
                    return Ok("Ok");
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
